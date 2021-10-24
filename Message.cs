using System;
using System.Collections.Generic;
using System.Text;

namespace Nabrosok
{
    class Message
    {
        string ID;//номер сообщения
        string content;//содержание
        string date;//дата-время
        string[] readBy;//кем прочитано
        string[]repliedFrom;//айди сообщений,захваченных для ответа текущим
        string[] attachments;//вложения
        bool edited;//Редачилось?
        bool deleted;//Удалено?
        string author;//Id юзера
        
        public Message(string id,string cont,string dt,string []rb,string[]rf,string[]attachmens,string au)
        {
            this.ID = id;
            this.content = cont;
            this.date = dt;
            this.readBy = rb;
            this.repliedFrom = rf;
            this.attachments = attachmens;
            edited = false;
            deleted = false;
            this.author = au;
        }
       
    }
}
