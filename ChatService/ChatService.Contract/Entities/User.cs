using System;

namespace СhatService.Contract
{
    public class User
    {
        public enum Role
        {
            Customer = 0,
            Vendor = 1,
            Support = 2,
            Admin = 10
        }

        string ID;
        string avatar;
        string name;
        string surname;
        Role[] roles;
        string[] chats;
        DateTime lastActivity;

        public User(
            string ID,
            string avatar,
            string name,
            string surname,
            Role[] roles,
            string[] chats,
            DateTime lastActivity
        )
        {
            this.ID = ID;
            this.avatar = avatar;
            this.name = name;
            this.surname = surname;
            this.roles = roles;
            this.chats = chats;
            this.lastActivity = lastActivity;     
        }
    }
   
}
