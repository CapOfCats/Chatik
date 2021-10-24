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
        {//Содаём юзеров
         //толкаем сюда говно в датабазу
         //Создаём чат по говну из датабазы
         //полученную инфу о чате толкаем в датабазу
         //имеем: Инфу о юзерах, о чате, каждое новое сообщение в методе создаёт экземпляр класса Message, которое тоже толкается в датабазу и возвращается оттуда
         //profit
             Chat GetChat(int id){
            //Условие:Where Id=id, SELECT. Из величин вызываем конструктор Chat-обьекта. Ретурним:
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
                //foreach(int id in IDs)
                //{UPDATE ...SET cont='Сообщение было удалено',repliedfrom=NULL,attachments=NULL,deleted=TRUE WHERE Id=id}
                //Апдейт
            }
            void EditMessage(int id,string text,int[]attachments,int[]repliedfrom)
            {
                //UPDATE ... cont='text',attachments=attachments,repliedFrom=repliedfrom,edited=TRUE WHERE ID=id;
                //Апдейтим на сервере
            }
            
        }
    }
}
