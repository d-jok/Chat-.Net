using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chatClient.User
{
    [Serializable]
    public class Info
    {
        public string Name;
        public string Surname;
        public string NickName;
        public string Email;
        public string Phone;

        public Info() { }

        public Info(string name, string sur, string nick, string email, string phone)
        {
            this.Name = name;
            this.Surname = sur;
            this.NickName = nick;
            this.Email = email;
            this.Phone = phone;
        }
    }
}
