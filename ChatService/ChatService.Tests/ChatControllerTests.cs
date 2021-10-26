
using NSubstitute;
using NUnit.Framework;
using System;
using СhatService.Contract;
using СhatService.Interfaces;




namespace СhatService.Tests
{

    public class SearchServiceTests
    {
        private IChatController chatController;
        private IMessageController messageController;
        private IUserConnectionController connectionController;

        [SetUp]
        public void SetUp()
        {
            chatController = Substitute.For<IChatController>();
            messageController = Substitute.For<IMessageController>();
            connectionController = Substitute.For<IUserConnectionController>();
        }

        [TearDown]
        public void TearDown()
        {
            chatController = null;
            messageController = null;
            connectionController = null;
        }

        [Test]//1
        public void TestGetChat()
        {
            string[] users = new string[1];
            string[] messages = new string[1];  
            // Arrange (Инициализация данных, подготовка к тесту)
            chatController
                .GetChat("user1", "chat1")
                .Returns(new Chat("ID", "title", users, messages));
            chatController
                .GetChat("wrongUser", "wrongChat")
                .Returns(x => { throw new Exception(); });
            chatController
                .GetChat("wrongUser", "chat1")
                .Returns(x => { throw new Exception(); });
            chatController
                .GetChat("user1", "wrongChat")
                .Returns(x => { throw new Exception(); });


            // Act (выполняем тестовую логику)
            Chat chat = chatController.GetChat("user1", "chat1");

            // Assert (сверка результата с ожиданиями)
            // Assert.That(chat, Is.Not.Null);
            Assert.That(chat.ID, Is.Not.Null);
            Assert.That(chat.ID, Is.Not.Empty);
            Assert.That(chat.title, Is.Not.Null);
            Assert.That(chat.title, Is.Not.Empty);
            Assert.That(chat.users, Is.Not.Null);
            Assert.That(chat.users, Is.Not.Empty);
            Assert.That(chat.messages, Is.Not.Null);
            Assert.That(chat.messages, Is.Not.Empty);
            Assert.Throws<Exception>(() => chatController.GetChat("wrongUser", "wrongChat"));
            Assert.Throws<Exception>(() => chatController.GetChat("wrongUser", "chat1"));
            Assert.Throws<Exception>(() => chatController.GetChat("user1", "wrongChat"));
        }
        [Test]//2
        public void TestMessage()
        {
            // Arrange
            messageController
                .GetMessages(1, 1, "user", "message")
                .Returns(new Message[1]);   
            messageController
                .GetMessages(0, 0, "wrongUser", "wrongChat")
                .Returns(x => { throw new Exception(); });
            messageController
                .GetMessages(1, 0, "wrongUser", "wrongChat")
                .Returns(x => { throw new Exception(); });            
            messageController
               .GetMessages(1, 0, "wrongUser", "chat")
               .Returns(x => { throw new Exception(); });
            messageController
               .GetMessages(1, 0, "user", "wrongChat")
               .Returns(x => { throw new Exception(); });
            messageController
               .GetMessages(1, 0, "user", "chat")
               .Returns(x => { throw new Exception(); });
            messageController
               .GetMessages(1, 1, "wrongUser", "wrongChat")
               .Returns(x => { throw new Exception(); });
            messageController
               .GetMessages(1, 1, "wrongUser", "chat") 
               .Returns(x => { throw new Exception(); });  
            messageController
               .GetMessages(0, 1, "wrongUser", "wrongChat")
               .Returns(x => { throw new Exception(); });
            messageController
              .GetMessages(0, 1, "wrongUser", "chat")
              .Returns(x => { throw new Exception(); });
            messageController
              .GetMessages(0, 1, "user", "wrongChat")
              .Returns(x => { throw new Exception(); });
            messageController
              .GetMessages(0, 1, "user", "chat")
              .Returns(x => { throw new Exception(); });
            messageController
              .GetMessages(0, 0, "user", "wrongChat")
              .Returns(x => { throw new Exception(); });
            messageController
              .GetMessages(0, 0, "user", "chat")
              .Returns(x => { throw new Exception(); });
            messageController
              .GetMessages(0, 0, "wrongUser", "chat")
              .Returns(x => { throw new Exception(); });
            //Act
            Message[] message = messageController.GetMessages(1, 1, "user1", "Chat1"); 
           
            //Assert
            Assert.That(message.ID, Is.Not.Null);
            Assert.That(message.ID, Is.Not.Empty);
            Assert.That(message.content, Is.Not.Null);
            Assert.That(message.content, Is.Not.Empty);
            Assert.That(message.date, Is.Not.Null);
            Assert.That(message.date, Is.Not.Empty);
            Assert.That(message.readBy, Is.Not.Empty);
            Assert.That(message.readBy, Is.Not.Null);
            Assert.That(message.repliedFrom, Is.Not.Null);
            Assert.That(message.repliedFrom, Is.Not.Empty);
            Assert.That(message.attachments, Is.Not.Null);
            Assert.That(message.attachments, Is.Not.Empty);
            Assert.That(message.edited, Is.Not.Null);
            Assert.That(message.edited, Is.Not.Empty);
            Assert.That(message.deleted, Is.Not.Null);
            Assert.That(message.deleted, Is.Not.Empty);
            Assert.That(message.author, Is.Not.Null);
            Assert.That(message.author, Is.Not.Empty);
            Assert.Throws<Exception>(() => messageController.GetMessages(0, 0, "wrongUser", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.GetMessages(1, 0, "wrongUser", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.GetMessages(1, 0, "wrongUser", "chat"));
            Assert.Throws<Exception>(() => messageController.GetMessages(1, 0, "user", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.GetMessages(1, 0, "user", "chat"));
            Assert.Throws<Exception>(() => messageController.GetMessages(1, 1, "wrongUser", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.GetMessages(1, 1, "wrongUser", "chat"));           
            Assert.Throws<Exception>(() => messageController.GetMessages(0, 1, "wrongUser", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.GetMessages(0, 1, "wrongUser", "chat"));
            Assert.Throws<Exception>(() => messageController.GetMessages(0, 1, "user", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.GetMessages(0, 1, "user", "chat"));
            Assert.Throws<Exception>(() => messageController.GetMessages(0, 0, "user", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.GetMessages(0, 0, "user", "chat"));
            Assert.Throws<Exception>(() => messageController.GetMessages(0, 0, "wrongUser", "chat"));
          

        }
        [Test]//3
        public void TestAddMessage() 
        {
            string[] Form = new string[2];
            Attachment[] Attech = new Attachment[3];
            //Arrange
           
            messageController
              .AddMessage("wrongText", Form, Attech, "user", "chat")
              .Do(x => { throw new Exception(); });
            messageController
              .AddMessage("wrongText", Form, Attech, "wrongUser", "chat")
              .Do(x => { throw new Exception(); });
            messageController
              .AddMessage("wrongText", Form, Attech, "wrongUser", "wrongChat")
             .Do(x => { throw new Exception(); });
            messageController
              .AddMessage("wrongText", Form, Attech, "user", "wrongChat")
              .Do(x => { throw new Exception(); });
            messageController
              .AddMessage("text", Form, Attech, "wrongUser", "wrongChat")
              .Do(x => { throw new Exception(); });
            messageController
              .AddMessage("text", Form, Attech, "user", "wrongChat")
              .Do(x => { throw new Exception(); });
            messageController
              .AddMessage("text", Form, Attech, "wrongUser", "chat")
              .Do(x => { throw new Exception(); });
            //Act
            Message message = messageController.AddMessage("text1", Form, Attech, "user1", "chat1");
            //Assert 
            Assert.That(message, Is.Not.Null);
            Assert.Throws<Exception>(() => messageController.AddMessage("wrongText", Form, Attech, "user", "chat"));
            Assert.Throws<Exception>(() => messageController.AddMessage("wrongText", Form, Attech, "wrongUser", "chat"));
            Assert.Throws<Exception>(() => messageController.AddMessage("wrongText", Form, Attech, "wrongUser", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.AddMessage("wrongText", Form, Attech, "user", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.AddMessage("text", Form, Attech, "wrongUser", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.AddMessage("text", Form, Attech, "user", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.AddMessage("text", Form, Attech, "wrongUser", "chat"));

        }
        [Test]//4
        public void TestEditMessage()
        {
            string[] repliedFrom = new string[2];
            object[] attachments = new object[2];
            //Arrange
          
            messageController
               .EditMessage("message", "text", attachments, repliedFrom, "user", "wrongChat")
               .Do(x => { throw new Exception(); });
            messageController
               .EditMessage("message", "text", attachments, repliedFrom, "wrongUser", "wrongChat")
               .Do(x => { throw new Exception(); });
            messageController
               .EditMessage("message", "text", attachments, repliedFrom, "wrongUser", "chat")
              .Do(x => { throw new Exception(); });
            messageController
               .EditMessage("message", "wrongText", attachments, repliedFrom, "user", "chat")
              .Do(x => { throw new Exception(); });
            messageController
               .EditMessage("message", "wrongText", attachments, repliedFrom, "user", "wrongChat")
              .Do(x => { throw new Exception(); });
            messageController
               .EditMessage("message", "wrongText", attachments, repliedFrom, "wrongUser", "chat")
               .Do(x => { throw new Exception(); });
            messageController
               .EditMessage("message", "wrongText", attachments, repliedFrom, "wrongUser", "wrongChat")
             .Do(x => { throw new Exception(); });
            messageController
               .EditMessage("wrongMessage", "text", attachments, repliedFrom, "user", "chat")
             .Do(x => { throw new Exception(); });
            messageController
               .EditMessage("wrongMessage", "text", attachments, repliedFrom, "user", "wrongChat")
             .Do(x => { throw new Exception(); });
            messageController
               .EditMessage("wrongMessage", "text", attachments, repliedFrom, "wrongUser", "wrongChat")
              .Do(x => { throw new Exception(); });
            messageController
               .EditMessage("wrongMessage", "text", attachments, repliedFrom, "wrongUser", "chat")
               .Do(x => { throw new Exception(); });
            messageController
               .EditMessage("wrongMessage", "wrongText", attachments, repliedFrom, "user", "chat")
            .Do(x => { throw new Exception(); });
            messageController
               .EditMessage("wrongMessage", "wrongText", attachments, repliedFrom, "wrongUser", "chat")
               .Do(x => { throw new Exception(); });
            messageController
               .EditMessage("wrongMessage", "wrongText", attachments, repliedFrom, "user", "wrongChat")
               .Do(x => { throw new Exception(); });
            messageController
               .EditMessage("wrongMessage", "wrongText", attachments, repliedFrom, "wrongUser", "wrongChat")
            .Do(x => { throw new Exception(); });
            //Act
            Message message = messageController.EditMessage("message1", "text1", attachments, repliedFrom, "user1", "chat1");
            //Assert 
            Assert.That(message, Is.Not.Null);
            Assert.Throws<Exception>(() => messageController.EditMessage("message", "text", attachments, repliedFrom, "user", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.EditMessage("message", "text", attachments, repliedFrom, "wrongUser", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.EditMessage("message", "text", attachments, repliedFrom, "wrongUser", "chat"));
            Assert.Throws<Exception>(() => messageController.EditMessage("message", "wrongText", attachments, repliedFrom, "user", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.EditMessage("message", "wrongText", attachments, repliedFrom, "user", "chat"));
            Assert.Throws<Exception>(() => messageController.EditMessage("message", "wrongText", attachments, repliedFrom, "wrongUser", "chat"));
            Assert.Throws<Exception>(() => messageController.EditMessage("message", "wrongText", attachments, repliedFrom, "wrongUser", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.EditMessage("wrongMessage", "text", attachments, repliedFrom, "user", "chat"));
            Assert.Throws<Exception>(() => messageController.EditMessage("wrongMessage", "text", attachments, repliedFrom, "user", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.EditMessage("wrongMessage", "text", attachments, repliedFrom, "wrongUser", "chat"));
            Assert.Throws<Exception>(() => messageController.EditMessage("wrongMessage", "text", attachments, repliedFrom, "wrongUser", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.EditMessage("wrongMessage", "wrongText", attachments, repliedFrom, "user", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.EditMessage("wrongMessage", "wrongText", attachments, repliedFrom, "user", "chat"));
            Assert.Throws<Exception>(() => messageController.EditMessage("wrongMessage", "wrongText", attachments, repliedFrom, "wrongUser", "chat"));
            Assert.Throws<Exception>(() => messageController.EditMessage("wrongMessage", "wrongText", attachments, repliedFrom, "wrongUser", "wrongChat"));
        }
        [Test]//5
        public void TestDeleteMessages()
        {
            string[] messages = new string[2];
            //Arrange
            
            messageController
               .DeleteMessages(messages, "wrongUser", "chat")
              .Do(x => { throw new Exception(); });
            messageController
               .DeleteMessages(messages, "user", "wrongChat")
              .Do(x => { throw new Exception(); });
            messageController
               .DeleteMessages(messages, "wrongUser", "wrongChat")
             .Do(x => { throw new Exception(); });
            //Act
            Message message = messageController.DeleteMessages(messages, "user1", "chat1");
            //Assert 
            Assert.That(message, Is.Not.Null);
            Assert.Throws<Exception>(() => messageController.DeleteMessages(messages, "wrongUser", "chat"));
            Assert.Throws<Exception>(() => messageController.DeleteMessages(messages, "user", "wrongChat"));
            Assert.Throws<Exception>(() => messageController.DeleteMessages(messages, "wrongUser", "wrongChat"));

        }
        [Test]//6
        public void TestConnect()
        {
            //Arrange
            
            connectionController
                .Connect(new UserConnection("user", "typing", "wrongChat", 1))
                .Do(x => { throw new Exception(); });
            connectionController
               .Connect(new UserConnection("user", "wrongTyping", "chat", 1))
               .Do(x => { throw new Exception(); });
            connectionController
               .Connect(new UserConnection("user", "wrongTyping", "wrongChat", 1))
               .Do(x => { throw new Exception(); });
            connectionController
               .Connect(new UserConnection("wrongUser", "typing", "chat", 1))
               .Do(x => { throw new Exception(); });
            connectionController
               .Connect(new UserConnection("wrongUser", "typing", "wrongChat", 1))
               .Do(x => { throw new Exception(); });
            connectionController
               .Connect(new UserConnection("wrongUser", "wrongTyping", "chat", 1))
               .Do(x => { throw new Exception(); });
            connectionController
               .Connect(new UserConnection("wrongUser", "wrongTyping", "wrongChat", 1))
               .Do(x => { throw new Exception(); });
            //Act
               Message connection = connectionController.Connect(new UserConnection("wrongUser1", "wrongTyping1", "wrongChat1", 1));
            //Assert
            Assert.That(connection, Is.Not.Null);
            Assert.Throws<Exception>(() => connectionController.Connect(new UserConnection("user", "typing", "wrongChat", 1)));
            Assert.Throws<Exception>(() => connectionController.Connect(new UserConnection("user", "wrongTyping", "chat", 1)));
            Assert.Throws<Exception>(() => connectionController.Connect(new UserConnection("user", "wrongTyping", "wrongChat", 1)));
            Assert.Throws<Exception>(() => connectionController.Connect(new UserConnection("wrongUser", "typing", "chat", 1)));
            Assert.Throws<Exception>(() => connectionController.Connect(new UserConnection("wrongUser", "typing", "wrongChat", 1)));
            Assert.Throws<Exception>(() => connectionController.Connect(new UserConnection("wrongUser", "wrongTyping", "chat", 1)));
            Assert.Throws<Exception>(() => connectionController.Connect(new UserConnection("wrongUser", "wrongTyping", "wrongChat", 1)));
        }
        [Test]//7
        public void TestDisconnect()
        {
            //Arrange
       
      
            connectionController
                .Disconnect(new UserConnection("user", "typing", "wrongChat", 1))
                .Do(x => { throw new Exception(); });
            connectionController
               .Disconnect(new UserConnection("user", "wrongTyping", "chat", 1))
               .Do(x => { throw new Exception(); });
            connectionController
               .Disconnect(new UserConnection("user", "wrongTyping", "wrongChat", 1))
               .Do(x => { throw new Exception(); });
            connectionController
               .Disconnect(new UserConnection("wrongUser", "typing", "chat", 1))
               .Do(x => { throw new Exception(); });
            connectionController
               .Disconnect(new UserConnection("wrongUser", "typing", "wrongChat", 1))
               .Do(x => { throw new Exception(); });
            connectionController
               .Disconnect(new UserConnection("wrongUser", "wrongTyping", "chat", 1))
               .Do(x => { throw new Exception(); });
            connectionController
               .Disconnect(new UserConnection("wrongUser", "wrongTyping", "wrongChat", 1))
               .Do(x => { throw new Exception(); });
            //Act
            Message connection = connectionController.Disconnect(new UserConnection("wrongUser1", "wrongTyping1", "wrongChat1", 1));
            //Assert
            Assert.That(connection, Is.Not.Null);
            Assert.Throws<Exception>(() => connectionController.Disconnect(new UserConnection("user", "typing", "wrongChat", 1)));
            Assert.Throws<Exception>(() => connectionController.Disconnect(new UserConnection("user", "wrongTyping", "chat", 1)));
            Assert.Throws<Exception>(() => connectionController.Disconnect(new UserConnection("user", "wrongTyping", "wrongChat", 1)));
            Assert.Throws<Exception>(() => connectionController.Disconnect(new UserConnection("wrongUser", "typing", "chat", 1)));
            Assert.Throws<Exception>(() => connectionController.Disconnect(new UserConnection("wrongUser", "typing", "wrongChat", 1)));
            Assert.Throws<Exception>(() => connectionController.Disconnect(new UserConnection("wrongUser", "wrongTyping", "chat", 1)));
            Assert.Throws<Exception>(() => connectionController.Disconnect(new UserConnection("wrongUser", "wrongTyping", "wrongChat", 1)));
        }
        [Test]//8
        public void TestSetTyping()
        {//Arrange
            connectionController
            
             
            connectionController
             .SetTyping(true, "wrongUser")
             .Do(x => { throw new Exception(); });
            connectionController
            .SetTyping(false, "user")
            .Do(x => { throw new Exception(); });
            connectionController
            .SetTyping(false, "wrongUser")
            .Do(x => { throw new Exception(); });
            //Act
            Message connection = connectionController.SetTyping(true, "user1");
            //Assert
            Assert.That(connection, Is.Not.Null);
            Assert.Throws<Exception>(() => connectionController.SetTyping(true, "wrongUser"));
            Assert.Throws<Exception>(() => connectionController.SetTyping(false, "wrongUser"));
            Assert.Throws<Exception>(() => connectionController.SetTyping(false, "user"));;

        }
    }

}