import React from 'react'
import css from './styles.module.sass'
import { IChat, IUser } from '../../types'
import cn from 'classnames'

interface IProps {
  chat: IChat | null
  anotherUser: IUser
  isChooseMode: boolean
  handleCloseButtonClick: (e: React.MouseEvent<HTMLButtonElement>) => any
  handleReplyClick: (e: React.MouseEvent<HTMLButtonElement>) => any
  handleDeleteClick: (e: React.MouseEvent<HTMLButtonElement>) => any
}

export default function Header({
  chat,
  anotherUser,
  isChooseMode,
  handleCloseButtonClick,
  handleReplyClick,
  handleDeleteClick,
}: IProps) {
  return (
    <div className={css.header}>
      <div className={css.row}>
        <div className={css.rowBlockFirst}>
          <img
            src={anotherUser.avatar}
            className={css.avatar}
          />
          <span className={css.title}>
            {chat?.title}
          </span>
        </div>
        <button
          className={css.close}
          onClick={e => handleCloseButtonClick(e)}
        >
          ×
        </button>
      </div>
      <div
        className={cn(css.row, css.tools, { [css.toolsActive]: isChooseMode, })}
      >
        <button className={css.tool} onClick={e => handleReplyClick(e)}>
          <img src="/images/reply.svg" /> Ответить
        </button>
        <button className={css.tool} onClick={e => handleDeleteClick(e)}>
          <img src="/images/trash.svg" /> Удалить
        </button>
      </div>
    </div>
  )
}