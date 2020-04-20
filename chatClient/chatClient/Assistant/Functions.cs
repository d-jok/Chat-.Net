using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace chatClient.Assistant
{
    class Functions
    {
        public void CommandDistributor(string command)
        {
            switch(command)
            {
                case "one":
                    MessageBox.Show("command");
                    break;
            }
        }
    }
}
