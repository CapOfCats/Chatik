import React, {
  useCallback, useEffect, useRef, useState
} from 'react'
import css from './styles.module.sass'
import {
  UpdateChat, UpdateMessages, UpdateUsers
} from '../../mocks'
import {
  IAttachment,
  IChat, IDeleteMessages, IMessage, ISendMessage, IUser
} from '../../types'
import {
  linkEntities,
  mapAttachments, mapChat, mapMessages, mapUsers
} from '../../utils'
import Header from '../Header'
import _ from 'lodash'
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr'
import { EMessagePosition, Message } from '../Message'
import Footer from '../Footer'
import Menu from '../Menu'

export interface IProps {
  isOpen: boolean
  setIsOpen: (value: boolean) => any
}

const currentUserID = '2'

export default function Chat({ isOpen, setIsOpen }: IProps) {
  const [chat, setChat] = useState<IChat | null>(null)
  const [messages, setMessages] = useState<IMessage[]>([])
  const [attachments, setAttachments] = useState<IAttachment[]>([])
  const [users, setUsers] = useState<IUser[]>([])

  const [isChooseMode, setIsChooseMode] = useState(false)
  const [repliedFrom, setRepliedFrom] = useState<IMessage['ID'][]>([])
  const [filesCount, setFilesCount] = useState(0)
  const [selectedMessages, setSelectedMessages] = useState<IMessage['ID'][]>([])
  const [activeMessage, setActiveMessage] = useState<IMessage['ID'] | null>(null)

  const [connection, setConnection] = useState<HubConnection | null>(null)

  const messagesRef = useRef<HTMLDivElement>(null)
  const messageTextRef = useRef<HTMLTextAreaElement>(null)
  const filesRef = useRef<HTMLInputElement>(null)

  useEffect(() => {
    // const newConnection = new HubConnectionBuilder()
    //   .withUrl(serverURL)
    //   .withAutomaticReconnect()
    //   .build()

    // setConnection(newConnection)

    // newConnection.start()
    //   .then(result => {
    //     console.log('Connected')

    //     newConnection.on('UpdateChat', ({ chat }) => {
    //       setChat(chat)
    //     })
    //   })
    //   .catch(e => console.log('Connection failed: ', e))

    const chat = mapChat(UpdateChat.chat)
    const messages = mapMessages(UpdateMessages.messages.reverse())
    const attachments = mapAttachments(UpdateMessages.attachments)
    const users = mapUsers(UpdateUsers.users)

    const linkedEntities = linkEntities(
      chat,
      messages,
      attachments,
      users
    )

    setChat(linkedEntities.chat)
    setMessages(linkedEntities.messages)
    setAttachments(linkedEntities.attachments)
    setUsers(linkedEntities.users)
  }, [])

  const startTyping = useCallback(
    _.debounce(
      () => {
        connection?.send('UserTyping', { typing: true })
          .catch(error => console.error(error))
      },
      1000,
      { leading: true, trailing: false }
    ),
    [connection]
  )

  const endTyping = useCallback(
    _.debounce(() => {
      connection?.send('UserTyping', { typing: false })
        .catch(error => console.error(error))
    }, 1000),
    [connection]
  )

  const handleMessageTextKeypress = (e: React.KeyboardEvent<HTMLTextAreaElement>) => {
    if (e.code === 'Enter' && (e.shiftKey || e.ctrlKey)) {
      e.preventDefault()
      sendMessage()
    } else {
      startTyping()
      endTyping()
    }
  }

  const sendMessage = async () => {
    if ((messageTextRef?.current?.value.trim() || filesCount) && connection && messageTextRef.current) {
      try {
        const files = await Promise.all(
          Array.from(filesRef.current?.files || []).map(
            (file) =>
              new Promise((resolve, reject) => {
                const reader = new FileReader()
                reader.readAsDataURL(file)
                reader.onload = () =>
                  resolve({
                    src: reader.result,
                    name: file.name,
                  })
                reader.onerror = (error) => reject(error)
              })
          )
        )

        connection.send('SendMessage', {
          text: messageTextRef.current?.value,
          repliedFrom: repliedFrom,
          attachments: files,
        } as ISendMessage)

        messageTextRef.current.value = ''
        clearFiles()
        setRepliedFrom([])
      } catch (error) {
        console.error(error)
      }
    }
  }

  const handleFilesChange = () => {
    setFilesCount(filesRef.current?.files?.length || 0)
  }

  const handleCloseButtonClick = () => {
    setIsOpen(false)
  }

  const handleReplyClick = () => {
    if (selectedMessages?.length) {
      console.log(selectedMessages)
      setRepliedFrom(selectedMessages)
      setSelectedMessages([])
      setIsChooseMode(false)
    } else if (activeMessage) {
      setRepliedFrom([activeMessage])
      setActiveMessage(null)
    }
  }

  const handleDeleteClick = () => {
    if (connection) {
      if (selectedMessages) {
        connection.send(
          'DeleteMessages',
          { IDs: [...selectedMessages] } as IDeleteMessages
        )
        setSelectedMessages([])
        setIsChooseMode(false)
      } else if (activeMessage) {
        connection.send(
          'DeleteMessages',
          { IDs: [activeMessage] } as IDeleteMessages
        )
        setActiveMessage(null)
      }
    }
  }

  const handleChooseMoreClick = () => {
    setActiveMessage(null)
    activeMessage && setSelectedMessages([activeMessage])
    setIsChooseMode(true)
  }

  const handleMessageTextClick = (e: React.MouseEvent, message: IMessage) => {
    if (isChooseMode) {
      if (selectedMessages.includes(message.ID)) {
        setSelectedMessages((prev) => {
          const filtered = prev.filter((item) => item !== message.ID)
          if (filtered.length === 0) setIsChooseMode(false)
          return filtered
        })
      } else setSelectedMessages((prev) => prev.concat([message.ID]))
    } else if (
      // TODO check
      // @ts-ignore
      e.target.tagName !== 'IMG' &&
      // @ts-ignore
      e.target.tagName !== 'BUTTON'
    ) {
      setActiveMessage(message.ID)
    }
  }

  const handleSelectionClick = (e: React.MouseEvent, message: IMessage) => {
    e.preventDefault()
    e.stopPropagation()
    if (!isChooseMode) return
    if (selectedMessages.includes(message.ID)) {
      setSelectedMessages((prev) => {
        const filtered = prev.filter((item) => item !== message.ID)
        if (filtered.length === 0) setIsChooseMode(false)
        return filtered
      })
    } else setSelectedMessages((prev) => prev.concat([message.ID]))
  }

  const handleRepliedFromClearClick = () => {
    setRepliedFrom([])
  }

  const handleRepliedFromMessageClick = (e: React.MouseEvent, message: IMessage) => {
    e.preventDefault()
    e.stopPropagation()
    const node = messagesRef.current?.querySelector(`[data-message-id="${message.ID}"]`)
    node?.scrollIntoView({ behavior: 'smooth' })
  }

  const clearFiles = (e?: React.MouseEvent) => {
    e?.preventDefault()
    setFilesCount(0)
    if (filesRef.current) filesRef.current.value = ''
  }

  if (!chat || !users.length || !messages.length) return null

  return (
    <div className={css.chat}>
      <Header
        anotherUser={users.find(i => i.ID !== currentUserID) as IUser}
        chat={chat}
        isChooseMode={isChooseMode}
        handleCloseButtonClick={handleCloseButtonClick}
        handleReplyClick={handleReplyClick}
        handleDeleteClick={handleDeleteClick}
      />
      <div className={css.messages} ref={messagesRef}>
        {messages.map((item, index) => (
          <Message
            key={item.ID}
            item={item}
            currentUser={currentUserID}
            selectedMessages={selectedMessages}
            handleSelectionClick={handleSelectionClick}
            handleMessageTextClick={handleMessageTextClick}
            handleRepliedFromMessageClick={handleRepliedFromMessageClick}
            position={
              index === messages.length - 1 || messages[index + 1].author !== item.author ?
                EMessagePosition.Top :
                index === 0 || messages[index - 1].author !== item.author ?
                  EMessagePosition.Bottom : EMessagePosition.Center
            }
          />
        ))}
        {activeMessage && (
          <Menu
            message={messages.find(i => i.ID === activeMessage) as IMessage}
            setActiveMessage={setActiveMessage}
            handleReplyClick={handleReplyClick}
            handleDeleteClick={handleDeleteClick}
            handleChooseMoreClick={handleChooseMoreClick}
          />
        )}
      </div>
      <Footer
        messageTextRef={messageTextRef}
        filesRef={filesRef}
        chat={chat}
        filesCount={filesCount}
        repliedFrom={repliedFrom}
        handleRepliedFromClearClick={handleRepliedFromClearClick}
        handleMessageTextKeypress={handleMessageTextKeypress}
        handleFilesChange={handleFilesChange}
        handleClearFilesButtonClick={clearFiles}
        handleSendMessageButtonClick={sendMessage}
      />
    </div>
  )
}