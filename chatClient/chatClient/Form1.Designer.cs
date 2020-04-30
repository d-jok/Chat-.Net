namespace chatClient
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.chatBox = new System.Windows.Forms.TextBox();
            this.ChatName = new System.Windows.Forms.Label();
            this.chat_msg = new System.Windows.Forms.TextBox();
            this.chat_send = new System.Windows.Forms.Button();
            this.ChatList = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chatBox
            // 
            this.chatBox.Location = new System.Drawing.Point(183, 48);
            this.chatBox.Multiline = true;
            this.chatBox.Name = "chatBox";
            this.chatBox.ReadOnly = true;
            this.chatBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.chatBox.Size = new System.Drawing.Size(605, 332);
            this.chatBox.TabIndex = 8;
            // 
            // ChatName
            // 
            this.ChatName.AutoSize = true;
            this.ChatName.Location = new System.Drawing.Point(180, 28);
            this.ChatName.Name = "ChatName";
            this.ChatName.Size = new System.Drawing.Size(37, 17);
            this.ChatName.TabIndex = 9;
            this.ChatName.Text = "Chat";
            // 
            // chat_msg
            // 
            this.chat_msg.Location = new System.Drawing.Point(183, 386);
            this.chat_msg.Multiline = true;
            this.chat_msg.Name = "chat_msg";
            this.chat_msg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chat_msg.Size = new System.Drawing.Size(474, 52);
            this.chat_msg.TabIndex = 10;
            // 
            // chat_send
            // 
            this.chat_send.Location = new System.Drawing.Point(670, 386);
            this.chat_send.Name = "chat_send";
            this.chat_send.Size = new System.Drawing.Size(118, 52);
            this.chat_send.TabIndex = 11;
            this.chat_send.Text = "Send";
            this.chat_send.UseVisualStyleBackColor = true;
            this.chat_send.Click += new System.EventHandler(this.chat_send_Click);
            // 
            // ChatList
            // 
            this.ChatList.FormattingEnabled = true;
            this.ChatList.ItemHeight = 16;
            this.ChatList.Location = new System.Drawing.Point(5, 45);
            this.ChatList.Name = "ChatList";
            this.ChatList.Size = new System.Drawing.Size(172, 388);
            this.ChatList.TabIndex = 12;
            this.ChatList.SelectedIndexChanged += new System.EventHandler(this.ChatList_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "List of users:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(5, 1);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(66, 28);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myProfileToolStripMenuItem,
            this.searchUserToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // myProfileToolStripMenuItem
            // 
            this.myProfileToolStripMenuItem.Name = "myProfileToolStripMenuItem";
            this.myProfileToolStripMenuItem.Size = new System.Drawing.Size(161, 26);
            this.myProfileToolStripMenuItem.Text = "My profile";
            this.myProfileToolStripMenuItem.Click += new System.EventHandler(this.myProfileToolStripMenuItem_Click);
            // 
            // searchUserToolStripMenuItem
            // 
            this.searchUserToolStripMenuItem.Name = "searchUserToolStripMenuItem";
            this.searchUserToolStripMenuItem.Size = new System.Drawing.Size(161, 26);
            this.searchUserToolStripMenuItem.Text = "Search User";
            this.searchUserToolStripMenuItem.Click += new System.EventHandler(this.searchUserToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(161, 26);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ChatList);
            this.Controls.Add(this.chat_send);
            this.Controls.Add(this.chat_msg);
            this.Controls.Add(this.ChatName);
            this.Controls.Add(this.chatBox);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ChatName;
        private System.Windows.Forms.TextBox chat_msg;
        private System.Windows.Forms.Button chat_send;
        private System.Windows.Forms.ListBox ChatList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem myProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchUserToolStripMenuItem;
        public System.Windows.Forms.TextBox chatBox;
    }
}

