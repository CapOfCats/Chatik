using System;
using System.Collections.Generic;
using System.Text;

namespace Nabrosok
{
    class Chat
    {
        string ID;//идентификатор чата
        string title;//название
        string[] users;//айдишники обитателей
        string[] messages;//список сообщений
        public Chat(string id,string tit,string[]uss,string[]mess)
        {
            this.ID=id;           
            this.title = tit;
            this.users = uss;
            this.messages = mess;
        }
    }
}
