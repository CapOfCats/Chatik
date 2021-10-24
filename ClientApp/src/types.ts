import { Moment } from 'moment'

export enum ERole {
  Customer = 0,
  Vendor = 1,
  Support = 2,
  Admin = 10,
}

export enum EAttachmentType {
  Image = 1
}

// Server

export interface IUserServer {
  ID: string
  avatar: string
  name: string
  surname: string
  roles: ERole[]
  typing: boolean
  online: boolean
}

export interface IChatServer {
  ID: string
  title: string
  users: IUser['ID'][]
  messages: IMessageServer['ID'][]
}

export interface IMessageServer {
  ID: string
  author: IUser['ID']
  content: string
  date: string
  readBy: IUser['ID'][]
  repliedFrom: IMessageServer['ID'][]
  attachments: IAttachmentServer['ID'][]
  edited: boolean
  deleted: boolean
}

export interface IAttachmentServer {
  ID: string
  type: EAttachmentType
  name: string
  src: string
  thumbnail: string
  width: number
  height: number
}

// Client

export interface IUser {
  ID: string
  avatar?: string
  name: string
  surname: string
  roles: ERole[]
  typing: boolean
  online: boolean
}

export interface IChat {
  ID: string
  title: string
  users: IUser[]
}

export interface IMessage {
  ID: string
  author: IUser
  content: string
  date: Moment
  readBy: IUser[]
  repliedFrom: IMessage[]
  attachments: IAttachment[]
  edited: boolean
  deleted: boolean
}

export interface IAttachment {
  ID: string
  type: EAttachmentType
  name: string
  src: string
  thumbnail: string
  width: number
  height: number
}

// Events

export type TClientEvent = 'GetChat' | 'GetMessages' | 'SendMessage' | 'DeleteMessages' | 'EditMessage'
export type TServerEvent = 'UpdateChat' | 'UpdateMessages' | 'UpdateUsers'

// Client events

export interface IGetChat {

}

export interface IGetMessages {
  offset: number
  count: number
}

export interface ISendMessage {
  text: string
  repliedFrom: string[]
  attachments: {
    name: string
    src: string
  }[]
}

export interface IDeleteMessages {
  IDs: string[]
}

export interface IEditMessage {
  ID: string
  text: string
  attachments: {
    name: string,
    src: string,
  }[]
  repliedFrom: string[]
}

// Server events

export interface IUpdateChat {
  chat: IChatServer
}

export interface IUpdateMessages {
  messages: IMessageServer[]
  attachments: IAttachmentServer[]
}

export interface IUpdateUsers {
  users: {
    ID: string
    name: string
    surname: string
    avatar: string
    roles: ERole[]
    typing: boolean
    online: boolean
  }[]
}