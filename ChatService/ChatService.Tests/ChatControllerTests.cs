using NSubstitute;
using NUnit.Framework;
using СhatService.Contract;
using СhatService.Interfaces;

namespace СhatService.Tests
{
    public class SearchServiceTests
    {
        private IChatController chatController;

        [SetUp]
        public void SetUp()
        {
            chatController = Substitute.For<IChatController>();
        }

        [TearDown]
        public void TearDown()
        {
            chatController = null;
        }

        [Test]
        public void TestGetChat()
        {
            // Arrange (Инициализация данных, подготовка к тесту)
            chatController
                .GetChat(/* Заполнить */)
                .Returns(new Chat(/* Заполнить */));

            // Act (выполняем тестовую логику)
            Chat chat = chatController.GetChat(/* Заполнить */);

            // Assert (сверка результата с ожиданиями)
            Assert.That(chat, Is.Not.Null);
            // ...
        }
    }
}