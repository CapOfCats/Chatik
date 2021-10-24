using System;
using Npgsql;

namespace Nabrosok
{
    class Program
    {
        static void Main(string[] args)
             public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        {
         
             Chat GetChat(int id){
            //Условие:Where Id=id, SELECT. Из величин вызываем конструктор Chat-обьекта. Возвращаем чат из БД Ретурним:
            //return Chat
            }
             Message[] GetMessages(int offset, int count) {
            //SELECT * FROM... LIMIT count OFFSET offset
            //Конструкторим и пихаем в массив
            // return Messages[]
            }
             void SendMessage (string text,int[]repliedFrom, int[]attachments, int author)
            {
                //Конструктор Message( text,datetime с сервера,readBy пустой, repliedFrom, attachments, author) 
                //Данные Message заносятся в бд
                //Апдейт
            }
            void DeleteMessages(int[]IDs)
            {
            //удаляет месседжи из БД
                //foreach(int id in IDs)
                //{UPDATE ...SET cont='Сообщение было удалено',repliedfrom=NULL,attachments=NULL,deleted=TRUE WHERE Id=id}
                //Апдейт
            }
            void EditMessage(int id,string text,int[]attachments,int[]repliedfrom)
            {
                //UPDATE ... cont='text',attachments=attachments,repliedFrom=repliedfrom,edited=TRUE WHERE ID=id;
                //Апдейтим на сервере
            }
        Chat UpdateChat(Chat chat)
        {
            //Сервер запрашивает чат.На вход -чат. На сервере встроен автообновщик. Однако, если что забагалось, исходя из БД метод возвращает нормальный чат(запуск конструктора чата с помощью методов GetMessages и пр.) и вставляет в клиент.
            //return chat
        }
       Message[] UpdateMessages(< ...Message, bool hide >[] messages, Attachment[] attachments)
            {
            //Сервер запрашивает сообщения.
            //На вход - Массив словарей по message-ключу и hide-значению тех сообщений,что нужно показать, если они родом из текущего чата(вместе с вложениями)
            // if(!hide) { Добавляем в цикле в массив типа Message[] }
            //После цикла return Message[];
        }
        Users[] UpdateUsers(< UUID ID, string name, string surname, bool typing, bool online, string avatar, byte[] roles >[] users)
        {
            //Сервер запрашивает юзеров
            //На вход - Данные о юзерах. Пересобирает их в конструкторе User() и возвращает массив этого типа.
            //return Users[]
        }
        void Connect(UserConnection connection)
        {
            //На вход - экземпляр класса connection,содержащий всё для подключения. На основе данных подключает юзера в хаб. 
        }
       void Disconnect(UserConnection connection)
        {
            //противоположное верхнему. Дропает юзера с сервера.
        }
        void AddMessage(string text, UUID[] repliedFrom, Attachment[] attachments, UUID user, UUID chat)
        {
            //Строит экземляр класса по конструктору Message()
            //отправляет данные в клиент( по айди чата на входе)
        }
        Message EditMessage(UUID message, string text, < name, src, type >[] attachments, UUID[] repliedFrom, UUID user, UUID chat)
        {
            //Редачит сообщение в клиенте.
            //Привычный конструктор, ребилд изменений - возвращение экземпляра message
            //return message
        }
        void DeleteMessages(UUID[] messages, UUID user, UUID chat)
        {
            //Удаляет мэсседжи в клиенте
            //На вход - UUID массив айдишников сообщений, айдишник автора, и айдишник чата
        }
        Chat GetChatС(UUID user, UUID chat)
        {
            //На вход айди юзера и чата
            //запрашивает у клиента вернуть чат по айди для юзера
            //ну и получает его в лицо
            //return Chat-экземпляр
        }
        Messages[] GetMessagesC(int offset, int count, UUID user, UUID chat)
        {
            //Запрашивает у клиента массив сообщений
            //На вход позиция, с которой начинается счет и количество сообщений. Айди юзера и чата. 
            //В цикле запихивает экземпляры в результирующий массив
            //ретурним messages[]
        }
        bool typing(bool isTyping, UUID user, UUID chat)
        {
            //Запрашивает у клиента, пишет ли юзер на данный момент
            // На вход айди юзера, чата и параметр isTyping
            //Возвращает всю правду об этом человеке в виде bool
            //return bool.
        }
      }
    }
}
