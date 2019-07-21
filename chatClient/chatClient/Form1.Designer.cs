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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.enterChat = new System.Windows.Forms.Button();
            this.exitChat = new System.Windows.Forms.Button();
            this.chatBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chat_msg = new System.Windows.Forms.TextBox();
            this.chat_send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(667, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(670, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(118, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "192.168.1.6";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(670, 74);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(118, 22);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "9933";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(667, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(670, 119);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(118, 22);
            this.userName.TabIndex = 5;
            this.userName.Text = "Sinned";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(667, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "User Name";
            // 
            // enterChat
            // 
            this.enterChat.Location = new System.Drawing.Point(670, 147);
            this.enterChat.Name = "enterChat";
            this.enterChat.Size = new System.Drawing.Size(118, 23);
            this.enterChat.TabIndex = 6;
            this.enterChat.Text = "Connect";
            this.enterChat.UseVisualStyleBackColor = true;
            this.enterChat.Click += new System.EventHandler(this.button1_Click);
            // 
            // exitChat
            // 
            this.exitChat.Location = new System.Drawing.Point(670, 176);
            this.exitChat.Name = "exitChat";
            this.exitChat.Size = new System.Drawing.Size(118, 23);
            this.exitChat.TabIndex = 7;
            this.exitChat.Text = "Disconnect";
            this.exitChat.UseVisualStyleBackColor = true;
            this.exitChat.Click += new System.EventHandler(this.exitChat_Click);
            // 
            // chatBox
            // 
            this.chatBox.Location = new System.Drawing.Point(12, 29);
            this.chatBox.Multiline = true;
            this.chatBox.Name = "chatBox";
            this.chatBox.ReadOnly = true;
            this.chatBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.chatBox.Size = new System.Drawing.Size(645, 351);
            this.chatBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Chat";
            // 
            // chat_msg
            // 
            this.chat_msg.Location = new System.Drawing.Point(12, 386);
            this.chat_msg.Multiline = true;
            this.chat_msg.Name = "chat_msg";
            this.chat_msg.Size = new System.Drawing.Size(645, 52);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chat_send);
            this.Controls.Add(this.chat_msg);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.exitChat);
            this.Controls.Add(this.enterChat);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button enterChat;
        private System.Windows.Forms.Button exitChat;
        private System.Windows.Forms.TextBox chatBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox chat_msg;
        private System.Windows.Forms.Button chat_send;
    }
}

