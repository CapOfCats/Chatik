using System;
using System.Collections.Generic;
using System.Text;

namespace Nabrosok
{
    class User
    {
        int ID;//Айди юзера
        string avatar;//Айди авы
        string name;//Имя
        string surname;//Фамилечко
        byte[] roles;//Список ролей++
        int[] chats;//В каких чатах есть
        string lastActivity;//Дата-время
        enum Role// какие бывают роли
        {
            Customer = 0,
            Vendor = 1,
            Support = 2,
            Admin = 10
        }
        public User(int id,string av,string nam,string sur,byte[]rols,int[]chas,string la)// Это контруктор... прикинь?
        {
            this.ID = id;
            this.avatar = av;
            this.name = nam;
            this.surname = sur;
            this.roles = rols;
            this.chats = chas;
            this.lastActivity = la;     
        }
    }
   
}
