using System;
using System.Collections.Generic;

namespace chatClient.Models
{
    [Serializable]
    class ListOfUsers
    {
        public int ListNumber;
        public string Name;
        public string Surname;
        public string NickName;
        public string Email;
        public string Phone;
        public List<Models.Message> messages;
        public DateTime DataOfChange;

        public ListOfUsers(int number, string name, string sur, string nick, 
            string email, string phone, List<Models.Message> messages, DateTime data)
        {
            ListNumber = number;
            Name = name;
            Surname = sur;
            NickName = nick;
            Email = email;
            Phone = phone;
            this.messages = messages;
            DataOfChange = data;
        }

        public ListOfUsers() { }
    }
}