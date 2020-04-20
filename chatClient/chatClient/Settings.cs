using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using chatClient.Models;

namespace chatClient
{
    public partial class Settings : Form
    {
        string path;
        Setting settings;

        public Settings(string path)
        {
            InitializeComponent();
            Form1 form = this.Owner as Form1;
            this.path = path;
            settings = new Setting();
            JObject jObject = JObject.Parse(File.ReadAllText(this.path));

            settings.AsisStatus = jObject.SelectToken("AsisStatus").Value<bool>();
            settings.MessageFont = jObject.SelectToken("MessageFont").Value<string>();
            settings.inputTextFont = jObject.SelectToken("inputTextFont").Value<string>();
            settings.NamesListOfUsers = jObject.SelectToken("NamesListOfUsers").Value<string>();

            printSettings();
        }

        private void assistantStatus(string status)
        {
            Form1 form = this.Owner as Form1;
            if (status == "On")
            {
                form.assistantStatus(true);
                settings.AsisStatus = true;
                printSettings();
                fileRewrite();
            }
            else
            {
                form.assistantStatus(false);
                settings.AsisStatus = false;
                printSettings();
                fileRewrite();
            }
        }

        private void changeMsgFont(string font)
        {
            Form1 form = this.Owner as Form1;
            form.chatBox.Font = new Font(font, 7.8f);
            settings.MessageFont = font;
            printSettings();
            fileRewrite();
        }

        private void fileRewrite()
        {
            try
            {
                using (StreamWriter file = File.CreateText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, settings);
                }
            }
            catch(JsonSerializationException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void printSettings()
        {
            if (settings.AsisStatus == true)
            {
                labelAsisStatus.ForeColor = Color.Green;
                labelAsisStatus.Text = "Active";
            }
            else
            {
                labelAsisStatus.ForeColor = Color.Red;
                labelAsisStatus.Text = "Disable";
            }

            labelMsgFontName.Text = settings.MessageFont;
            labelInputFont.Text = settings.inputTextFont;
        }

        private void changeNamesInList(string option)
        {
            Form1 form = this.Owner as Form1;

            switch (option)
            {
                case "Nick":
                    settings.NamesListOfUsers = option;
                    fileRewrite();
                    printSettings();
                    form.ChatListShow = "Nick";
                    form.ChatListItemsShow();
                    break;
                case "Phone":
                    settings.NamesListOfUsers = option;
                    fileRewrite();
                    printSettings();
                    form.ChatListShow = "Phone";
                    form.ChatListItemsShow();
                    break;
                case "Name":
                    settings.NamesListOfUsers = option;
                    fileRewrite();
                    printSettings();
                    form.ChatListShow = "Name";
                    form.ChatListItemsShow();
                    break;
                case "Surname":
                    settings.NamesListOfUsers = option;
                    fileRewrite();
                    printSettings();
                    form.ChatListShow = "Surname";
                    form.ChatListItemsShow();
                    break;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //OTHER FUNCTIONS
        private void timesNewRomanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeMsgFont("Times New Roman");
        }

        private void rageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeMsgFont("Rage");
        }

        private void microsoftSansSefirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeMsgFont("Microsoft Sans Serif");
        }

        private void yesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            assistantStatus("On");
        }

        private void noToolStripMenuItem_Click(object sender, EventArgs e)
        {
            assistantStatus("Off");
        }

        private void nickNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeNamesInList("Nick");
        }

        private void phoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeNamesInList("Phone");
        }

        private void nameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeNamesInList("Name");
        }

        private void surnaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeNamesInList("Surname");
        }
    }
}
