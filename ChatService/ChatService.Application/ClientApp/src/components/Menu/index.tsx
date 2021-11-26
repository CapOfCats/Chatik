import React, { Dispatch, SetStateAction } from 'react'
import { IMessage } from '../../types'
import css from './styles.module.sass'
import cn from 'classnames'

interface IProps {
  message: IMessage
  setActiveMessage: Dispatch<SetStateAction<number | null>>
  handleReplyClick: (e: React.MouseEvent<HTMLButtonElement>, message: IMessage) => any
  handleDeleteClick: (e: React.MouseEvent<HTMLButtonElement>, message: IMessage) => any
  handleChooseMoreClick: (e: React.MouseEvent<HTMLButtonElement>, message: IMessage) => any
}

export default function Menu({
  message,
  setActiveMessage,
  handleReplyClick,
  handleDeleteClick,
  handleChooseMoreClick
}: IProps) {
  return (
    <div className={css.wrapper}
      onClick={e => {
        e.preventDefault()
        e.stopPropagation()
        setActiveMessage(null)
      }}
    >
      <div className={css.menu}>
        <div className={cn(css.item, css.itemDisabled)}>
        Пользователей прочитало: {message.readBy.length}
        </div>
        <button className={css.item} onClick={e => handleReplyClick(e, message)}>
          <img src="/images/reply.svg" className={css.icon} />
        Ответить
        </button>
        <button className={css.item} onClick={e => handleDeleteClick(e, message)}>
          <img src="/images/trash.svg" className={css.icon} />
        Удалить
        </button>
        <hr />
        <button className={css.item} onClick={e => handleChooseMoreClick(e, message)}>
          <img src="/images/more.svg" className={css.icon} />
        Ещё
        </button>
      </div>
    </div>
  )
}