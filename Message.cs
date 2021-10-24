using System;
using System.Collections.Generic;
using System.Text;

namespace Nabrosok
{
    class Message
    {
        int ID;//номер сообщения
        string content;//содержание
        string date;//дата-время
        int[] readBy;//кем прочитано
        int[]repliedFrom;//айди сообщений,захваченных для ответа текущим
        int[] attachments;//вложения
        bool edited;//Редачилось?
        bool deleted;//Удалено?
        int author;//Id юзера
        
        public Message(string cont,string dt,int []rb,int[]rf,int[]attachmens,int au)
        {
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
