using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using chatClient.Assistant;

namespace chatClient.Models
{
    [Serializable]
    class Setting
    {
        public bool AsisStatus;
        public Speaker Speak;
        public string MessageFont;
        public string inputTextFont;
        public string NamesListOfUsers;

        public Setting() { }

        public Setting(bool status, Speaker sp, string msgFont, string inTextFont, string NLOU)
        {
            this.AsisStatus = status;
            this.Speak = sp;
            this.MessageFont = msgFont;
            this.inputTextFont = inTextFont;
            this.NamesListOfUsers = NLOU;
        }
    }
}
