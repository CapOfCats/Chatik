import { serverDateFormat } from './constants'
import {
  IAttachment,
  IAttachmentServer, IChat, IChatServer, IMessage, IMessageServer, IUser, IUserServer
} from './types'
import moment from 'moment'

export const mapChat = (chat: IChatServer) => {
  return { ...chat }
}

export const mapMessages = (messages: IMessageServer[]) => {
  return messages.map(item => ({
    ...item,
    date: moment(item.date, serverDateFormat)
  }))
}

export const mapAttachments = (attachments: IAttachmentServer[]) => {
  return attachments.map(item => ({ ...item }))
}

export const mapUsers = (users: IUserServer[]) => {
  return users.map(item => ({ ...item, }))
}

export const linkEntities = (
  chat: ReturnType<typeof mapChat>,
  messages: ReturnType<typeof mapMessages>,
  attachments: ReturnType<typeof mapAttachments>,
  users: ReturnType<typeof mapUsers>
) => {
  const chatCopy: any = { ...chat }
  const messagesCopy: any = messages.map(i => ({ ...i }))
  const attachmentsCopy: any = attachments.map(i => ({ ...i }))
  const usersCopy: any = users.map(i => ({ ...i }))

  chatCopy.users = chatCopy.users.map(
    (item: any) => usersCopy.find((i: any) => i.ID === item) as unknown as IUser
  )

  messagesCopy.forEach((item: any) => {
    item.author = usersCopy.find((i: any) => i.ID === item.author)
    item.readBy = item.readBy.map((i: any) => usersCopy.find((u: any) => u.ID === i))
    item.repliedFrom = item.repliedFrom.map((i: any) => messagesCopy.find((m: any) => m.ID === i))
    item.attachments = item.attachments.map((i: any) => attachmentsCopy.find((a: any) => a.ID === i))
  })

  return {
    chat: chatCopy as IChat,
    messages: messagesCopy as IMessage[],
    attachments: attachmentsCopy as IAttachment[],
    users: usersCopy as IUser[]
  }
}