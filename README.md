# Entity:
- **User**
- **Chat**
- **Massage**
- **Attachment**
- **UserConnection**


### User
- **ID** - UUID
- **avatar** - string # src
- **name** - char[42]
- **surname** - char[42]
- **roles** - byte[] # Enum Role
- **chats** - UUID[]
- **chatTyping** - UUID
- **lastActivity** - DateTime

#### Role
- **Customer** = 0
- **Vendor** = 1
- **Support** = 2
- **Admin** = 10

### Chat
- **ID** - UUID
- **title** - chars[64]
- **users** - UUID[]
- **messages** - UUID[]


### Message
- **ID** - UUID
- **content** - chars[1024]
- **date** - DateTime # Server time
- **readBy** - UUID[] # Users
- **repliedFrom** - UUID[] # Messages
- **attachments** - UUID[]
- **edited** - bool
- **deleted** - bool


### Attachment
- **ID** - UUID
- **type** - byte # enum AttachmentType
- **name** - chars[64]
- **src** - string

#### AttachmentType
- **Image** = 1


### UserConnection
- **connection** - default connection object
- **user** - UUID
- **chat** - UUID
- **messagesCount** - int # Количество сообщений, которое пользователь видит


# Events:

## Client:
- GetChat()
- GetMessages(int offset, int count)
- SendMessage(string text, UUID[] repliedFrom, < name, src >[] attachments)
- DeleteMessage(UUID[] messages)
- EditMessage(UUID message, string text, < name, src >[] attachmens, UUID[] repliedFrom)

## Server:
- UpdateChat(Chat chat)
- UpdateMessages(Message[] messages, Attachment[] attachments)
- UpdateUsers(< bool online, string name, string surname, bool typing >[])


# Controller:
- Connect(UserConnection connection)
- Disconnect(UserConnection connection)
- AddMessage(string text, UUID[] repliedFrom, Attachment[] attachments, UUID user, UUID chat)
- EditMessage(UUID message, string text, < name, src, type >[] attachments, UUID[] repliedFrom, UUID user, UUID chat)
- DeleteMessages(UUID[] messages, UUID user, UUID chat)
- GetChat(UUID user, UUID chat)
- GetMessages(int offset, int count, UUID user, UUID chat)
- typing(bool isTyping, UUID user, UUID chat)
