using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using chatClient.User;

namespace chatClient
{
    public partial class UserProfile : Form
    {
        private Info info;

        public UserProfile(Info user)
        {
            InitializeComponent();
            info = user;
            textName.Text = user.Name;
            textSurname.Text = user.Surname;
            textNick.Text = user.NickName;
            textEmail.Text = user.Email;
            textPhone.Text = user.Phone;
        }

        private void UserCabinet_Load(object sender, EventArgs e)
        {

        }

        private void SendNewInfo()
        {
            Form1 form = this.Owner as Form1;
            string request = "";
            string fields = "";

            if (textName.Text != info.Name)
            {
                request += textName.Text + " ";
                fields += "Name/";
            }
            if (textSurname.Text != info.Surname)
            {
                request += textSurname.Text + " ";
                fields += "Surname/";
            }
            if (textNick.Text != info.NickName)
            {
                request += textNick.Text + " ";
                fields += "NickName/";
            }
            if (textEmail.Text != info.Email)
            {
                request += textEmail.Text + " ";
                fields += "Email/";
            }
            if (textPhone.Text != info.Phone)
            {
                request += textPhone.Text + " ";
                fields += "Phone/";
            }
            if (textOldPassword.Text != "" && textNewPassword.Text != "")
            {
                request += textOldPassword.Text + "/" + textNewPassword.Text;
                fields += "Password/";
            }

            if(request != "")
            {
                string temp = "#Change " + form.GetUserInfo.Phone + " " + fields + " ";
                temp += request;
                form.send(temp);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            textName.ReadOnly = false;
            textSurname.ReadOnly = false;
            textNick.ReadOnly = false;
            textEmail.ReadOnly = false;
            textPhone.ReadOnly = false;
            textOldPassword.ReadOnly = false;
            textNewPassword.ReadOnly = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            SendNewInfo();
        }
    }
}
