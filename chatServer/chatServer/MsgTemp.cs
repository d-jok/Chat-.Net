using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chatServer
{
    class MsgTemp
    {
        public string User;
        public string Msg;
        public DateTime DateOfChange;

        MsgTemp() { }

        MsgTemp(string user, string msg, DateTime date)
        {
            this.User = user;
            this.Msg = msg;
            this.DateOfChange = date;
        }
    }
}
