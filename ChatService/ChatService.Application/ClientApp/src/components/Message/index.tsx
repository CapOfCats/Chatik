import React, { useState } from 'react'
import css from './styles.module.sass'
import cn from 'classnames'
import {
  EAttachmentType, IMessage, IUser
} from '../../types'
import Images from '../Images'
import {
  dateFormat,
  fullDateFormat,
  timeFormat,
} from '../../constants'

export enum EMessagePosition {
  Top,
  Center,
  Bottom
}

interface IProps {
  currentUser: IUser['ID']
  item: IMessage
  selectedMessages: IMessage['ID'][]
  position: EMessagePosition
  handleSelectionClick: (e: React.MouseEvent, message: IMessage) => any
  handleMessageTextClick: (e: React.MouseEvent<HTMLSpanElement>, message: IMessage) => any
  handleRepliedFromMessageClick: (e: React.MouseEvent<HTMLSpanElement>, message: IMessage) => any
}

export function Message({
  currentUser,
  item,
  selectedMessages,
  position,
  handleSelectionClick,
  handleMessageTextClick,
  handleRepliedFromMessageClick,
}: IProps) {
  const [isRepliedFromMessagesOpen, setIsRepliedFromMessagesOpen] = useState(item.repliedFrom.length < 4)

  const images = item.attachments.filter(item => item.type === EAttachmentType.Image)

  const toggleIsRepliedMessagesOpen = (e: React.MouseEvent<HTMLButtonElement>) => {
    setIsRepliedFromMessagesOpen(prev => !prev)
  }

  return (
    <div
      className={cn(
        css.message,
        { [css.messageMy]: item.author.ID === currentUser }
      )}
      data-message-id={item.ID}
    >
      <input
        type="radio"
        key={selectedMessages.includes(item.ID) ? 'checked' : ''}
        className={cn(
          css.checkbox,
          { [css.checkboxActive]: selectedMessages?.length, }
        )}
        checked={selectedMessages.includes(item.ID)}
        onClick={e => handleSelectionClick(e, item)}
        onChange={() => {}}
      />
      <div className={css.messageContent}>
        {
          item.author.ID !== currentUser &&
          (position === EMessagePosition.Bottom ? (
            <img
              src={item.author.avatar}
              className={css.avatar}
            />
          ) : <div className={css.avatar} />)
        }
        <div
          className={css.textWrapper}
          onClick={e => handleMessageTextClick(e, item)}
        >
          <div
            className={css.text}
          >
            {
              position === EMessagePosition.Top && item.author.ID !== currentUser && (
                <span className={css.name}>
                  {item.author.name} {item.author.surname}
                </span>
              )
            }
            {item.content}
            <Images images={images} />
            {
              position === EMessagePosition.Bottom && (
                <span className={css.time}>
                  {/* TODO better format */}
                  {item.date.format(timeFormat)}
                </span>
              )
            }
            {
              !!item.repliedFrom?.length && (
                <button
                  onClick={(e) => toggleIsRepliedMessagesOpen(e)}
                  className={
                    cn(
                      css.repliedFrom,
                      { [css.repliedFromActive]: isRepliedFromMessagesOpen }
                    )
                  }
                >
                Отвеченные сообщения: {item.repliedFrom.length}
                </button>
              )
            }
            {
              !!item.repliedFrom.length && (
                <div
                  className={cn(css.repliedFromMessages, { [css.repliedFromMessagesActive]: isRepliedFromMessagesOpen })}
                  style={{ maxHeight: isRepliedFromMessagesOpen ? 50 * item.repliedFrom.length : 0 }}
                >
                  {
                    item.repliedFrom.map((i, index) => (
                      <div
                        key={i.ID}
                        className={cn(
                          css.repliedFromMessage,
                          { [css.repliedFromMessageMy]: item.author.ID === currentUser, }
                        )}
                        onClick={(event) => handleRepliedFromMessageClick(event, i)}
                      >
                        {
                          (index === 0 || i.author !== item.repliedFrom[index - 1].author) && (
                            <p className={css.repliedFromMessageInfo}>
                              <span className={css.repliedFromMessageName}>{i.author.name}</span>
                              <span className={css.repliedFromMessageTime}>{i.date.format(timeFormat)}</span>
                            </p>
                          )
                        }
                        {i.content}
                        {!!item.attachments?.length && (
                          <p className={css.repliedFromAttachments}>Вложения: {item.attachments.length}</p>
                        )}
                      </div>
                    ))
                  }
                </div>
              )
            }
          </div>
        </div>
      </div>
    </div>
  )
}