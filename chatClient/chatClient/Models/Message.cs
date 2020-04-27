using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chatClient.Models
{
    [Serializable]
    public class Message
    {
        public string User;
        public string Text;

        public Message(string user, string text)
        {
            this.User = user;
            this.Text = text;
        }

        public Message() { }
    }
}
