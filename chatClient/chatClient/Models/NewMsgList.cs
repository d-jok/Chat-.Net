using System;
using System.Collections.Generic;

namespace chatClient.Models
{
    [Serializable]
    public class NewMsgList
    {
        public string number;
        public List<Message> msgList;

        public NewMsgList() { }

        public NewMsgList(string number, List<Message> msgList)
        {
            this.number = number;
            this.msgList = msgList;
        }
    }
}
