using System;
using System.Collections.Generic;
using System.Text;

namespace Nabrosok
{
    class UserConnection
    {
        //Connection obj;
        string user;
        string typing;
        string chat;
        int messagesCount;//Кол-во сообщений,которое юзер видит
        public UserConnection(string us,string typ,string ch,int mc)
        {
            this.user = us;
            this.typing = typ;
            this.chat = ch;
            this.messagesCount = mc;
        }
    }
}
