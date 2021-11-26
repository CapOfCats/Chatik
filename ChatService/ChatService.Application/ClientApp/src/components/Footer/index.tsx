import React, { useRef } from 'react'
import css from './styles.module.sass'
import {
  IChat, IMessage, IUser
} from '../../types'
import cn from 'classnames'
import TextareaAutosize from 'react-autosize-textarea'

export interface IProps {
  messageTextRef: React.MutableRefObject<HTMLTextAreaElement | null>
  filesRef: React.MutableRefObject<HTMLInputElement | null>
  chat: IChat | null
  filesCount: number
  repliedFrom: IMessage['ID'][],
  handleMessageTextKeypress: (e: React.KeyboardEvent<HTMLTextAreaElement>) => any
  handleFilesChange: (e: React.ChangeEvent<HTMLInputElement>) => any
  handleClearFilesButtonClick: (e: React.MouseEvent<HTMLSpanElement>) => any
  handleSendMessageButtonClick: (e: React.MouseEvent<HTMLButtonElement>) => any
  handleRepliedFromClearClick: (e: React.MouseEvent<HTMLButtonElement>) => any
}

export default function Footer({
  messageTextRef,
  chat,
  filesRef,
  filesCount,
  repliedFrom,
  handleMessageTextKeypress,
  handleFilesChange,
  handleClearFilesButtonClick,
  handleSendMessageButtonClick,
  handleRepliedFromClearClick,
}: IProps) {
  return (
    <div className={cn(css.footer, { [css.footerRepliedFrom]: repliedFrom.length })}>
      <div className={cn(css.repliedFrom, { [css.repliedFromActive]: repliedFrom.length })}>
        Ответ: {repliedFrom.length}
        <button className={css.repliedFromClear} onClick={handleRepliedFromClearClick}>×</button>
      </div>
      <TextareaAutosize
        className={css.textInput}
        maxRows={4}
        ref={messageTextRef}
        onKeyPress={e => handleMessageTextKeypress(e)}
      />
      <div className={css.buttons}>
        <label
          className={cn(css.button, css.buttonPhoto)}
        >
          <input
            type="file"
            accept="image/*"
            ref={filesRef}
            onChange={handleFilesChange}
            multiple
            hidden
          />
          {!!filesCount && (
            <span className={cn(css.badge, css.badgeSuccess)}>
              {filesCount}
            </span>
          )}
          <img
            src="/images/photo.svg"
            className={css.icon}
          />
          {!!filesCount && (
            <span
              className={cn(css.badge, css.badgeError, css.badgeBottom, css.badgeClose)}
              onClick={handleClearFilesButtonClick}
            >
              ×
            </span>
          )}
        </label>
        <button
          type="button"
          className={cn(css.button, css.buttonSend)}
          onClick={handleSendMessageButtonClick}
        >
          <img
            src="/images/send.svg"
            className={css.icon}
          />
        </button>
      </div>
    </div>
  )
}