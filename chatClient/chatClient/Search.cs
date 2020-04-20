using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace chatClient
{
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Form1 form = this.Owner as Form1;
            form.Search(textNumber.Text);
            textNumber.Text = "";
        }
    }
}
