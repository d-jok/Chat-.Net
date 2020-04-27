using System;
using System.Collections.Generic;
using chatClient.User;

namespace chatClient.Models
{
    [Serializable]
    public class ListOfUsers : Info
    {
        public int ListNumber;
        //Name, Surname, ... were HERE!!!
        public List<Models.Message> messages;
        public bool NewMsg;
        public DateTime DataOfChange;

        public ListOfUsers() { }

        public ListOfUsers(int number, string name, string sur, string nick, 
            string email, string phone, List<Models.Message> messages, bool newMsg, DateTime data)
        {
            ListNumber = number;
            Name = name;
            Surname = sur;
            NickName = nick;
            Email = email;
            Phone = phone;
            this.messages = messages;
            this.NewMsg = newMsg;
            DataOfChange = data;
        }
    }
}