using System;
using System.Collections.Generic;
using System.Text;

namespace Nabrosok
{
    class Chat
    {
        int ID;//идентификатор чата
        string title;//название
        int[] users;//айдишники обитателей
        int[] messages;//список сообщений
        public Chat(int id,string tit,int[]uss,int[]mess)
        {
            this.ID=id;           
            this.title = tit;
            this.users = uss;
            this.messages = mess;
        }
    }
}
