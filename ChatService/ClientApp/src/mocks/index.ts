import {
  EAttachmentType, ERole, IUpdateChat, IUpdateMessages, IUpdateUsers
} from '../types'

export const UpdateChat: IUpdateChat = {
  chat: {
    ID: 1,
    messages: [
      1,
      2,
      3
    ],
    title: 'Chat name',
    users: [1, 2]
  }
}

export const UpdateMessages: IUpdateMessages = {
  messages: [
    {
      ID: 1,
      attachments: [1],
      author: 1,
      content: 'Hello, John',
      date: '24.10.2021 19:00',
      deleted: false,
      edited: false,
      readBy: [1, 2],
      repliedFrom: []
    },
    {
      ID: 2,
      attachments: [1, 2],
      author: 1,
      content: 'Hello, John 2',
      date: '24.10.2021 19:00',
      deleted: false,
      edited: false,
      readBy: [1, 2],
      repliedFrom: []
    },
    {
      ID: 3,
      attachments: [],
      author: 2,
      content: 'Hello, John',
      date: '24.10.2021 19:01',
      deleted: false,
      edited: false,
      readBy: [1, 2],
      repliedFrom: []
    },
    {
      ID: 4,
      attachments: [],
      author: 2,
      content: 'repliedFrom',
      date: '24.10.2021 19:01',
      deleted: false,
      edited: false,
      readBy: [1, 2],
      repliedFrom: [
        1,
        2,
        3
      ],
    },
    {
      ID: 5,
      attachments: [],
      author: 2,
      content: 'Many repliedFrom',
      date: '24.10.2021 19:01',
      deleted: false,
      edited: false,
      readBy: [1, 2],
      repliedFrom: [
        1,
        2,
        3,
        4
      ]
    }
  ],
  attachments: [
    {
      ID: 1,
      name: 'First attachment',
      src: 'https://static.scientificamerican.com/sciam/cache/file/0B4ED7B8-6C6A-4031-BEE1253D115FD0CC_source.jpg',
      thumbnail: '',
      type: EAttachmentType.Image,
      width: 1122,
      height: 743,
    },
    {
      ID: 2,
      name: 'Second attachment',
      src: 'https://i.natgeofe.com/n/8271db90-5c35-46bc-9429-588a9529e44a/raccoon_thumb_3x4.JPG',
      thumbnail: '',
      type: EAttachmentType.Image,
      width: 567,
      height: 757,
    }
  ]
}

export const UpdateUsers: IUpdateUsers = {
  users: [
    {
      ID: 1,
      name: 'John',
      surname: 'Snow',
      avatar: 'https://html5css.ru/howto/img_avatar.png',
      roles: [ERole.Vendor],
      online: true,
      typing: true
    },
    {
      ID: 2,
      name: 'John2',
      surname: 'Doe',
      avatar: 'https://html5css.ru/howto/img_avatar.png',
      roles: [ERole.Customer],
      online: true,
      typing: false
    },
  ]
}