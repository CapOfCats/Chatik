# Entity:
- **User**
- **Chat**
- **Massage**
- **Attachment**
- **UserConnection**


### User
- **ID** - UUID
- **Avatar** - string # src
- **Name** - char[42]
- **Surname** - char[42]
- **Roles** - byte[] # Enum Role
- **Chats** - UUID[]
- **ChatTyping** - UUID
- **LastActivity** - DateTime

#### Role
- **Customer** = 0
- **Vendor** = 1
- **Support** = 2
- **Admin** = 10

### Chat
- **ID** - UUID
- **Title** - chars[64]
- **Users** - UUID[]
- **Messages** - UUID[]


### Message
- **ID** - UUID
- **Content** - chars[1024]
- **Date** - DateTime # Server time
- **ReadBy** - UUID[] # Users
- **RepliedFrom** - UUID[] # Messages
- **Attachments** - UUID[]
- **Edited** - bool
- **Deleted** - bool


### Attachment
- **ID** - UUID
- **Type** - byte # enum AttachmentType
- **Name** - chars[64]
- **Src** - string

#### AttachmentType
- **Image** = 1


### UserConnection
- **Сonnection** - default connection object
- **User** - UUID
- **Chat** - UUID
- **MessagesCount** - int # Количество сообщений, которое пользователь видит


# Events:

## Client:
- GetChat()
- GetMessages(int Offset, int Count)
- SendMessage(string Text, UUID[] RepliedFrom, < Name, Src >[] Attachments)
- DeleteMessage(UUID[] Messages)
- EditMessage(UUID Message, string Text, < Name, Src >[] Attachmens, UUID[] RepliedFrom)

## Server:
- UpdateChat(Chat Chat)
- UpdateMessages(Message[] Messages)
- UpdateUsers(< bool Online, string Name, string Surname, bool Typing >[])


# Controller:
- Connect(UserConnection Connection)
- Disconnect(UserConnection Connection)
- AddMessage(string Text, UUID[] RepliedFrom, Attachment[] Attachments, UUID User, UUID Chat)
- EditMessage(UUID Message, string Text, < Name, Src, Type >[] Attachments, UUID[] RepliedFrom, UUID User, UUID Chat)
- DeleteMessages(UUID[] Messages, UUID User, UUID Chat)
- GetChat(UUID User, UUID Chat)
- GetMessages(int Offset, int Count, UUID User, UUID Chat)
- typing(bool IsTyping, UUID User, UUID Chat)
