using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СhatService.Contract
{
   
        static class UserConnectionController
    {
        static void Connect(UserConnection connection)
        {
           //коннектит по экземпляру ЮзерКон
        }
        static void Disconnect(UserConnection connection)
        {
           //дропает
        }
        static void SetTyping(bool isTyping)
        {
            //запрос от сервера
        }
    }
}
