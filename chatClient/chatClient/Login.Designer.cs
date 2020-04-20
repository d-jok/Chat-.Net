namespace chatClient
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textEmail = new System.Windows.Forms.TextBox();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.ErrEmail = new System.Windows.Forms.Label();
            this.ErrPassword = new System.Windows.Forms.Label();
            this.StatusTitle = new System.Windows.Forms.Label();
            this.StatusText = new System.Windows.Forms.Label();
            this.RememberMe = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(384, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "LOGIN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Email:";
            // 
            // textEmail
            // 
            this.textEmail.Location = new System.Drawing.Point(293, 91);
            this.textEmail.Name = "textEmail";
            this.textEmail.Size = new System.Drawing.Size(226, 22);
            this.textEmail.TabIndex = 2;
            this.textEmail.Text = "potapenko5100@gmail.com";
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(293, 136);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(226, 22);
            this.textPassword.TabIndex = 4;
            this.textPassword.Text = "andrey1998";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(290, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Password:";
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(293, 164);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(226, 28);
            this.LoginButton.TabIndex = 5;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(290, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(178, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Does not have an account:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(468, 195);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(51, 17);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Sing In";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // ErrEmail
            // 
            this.ErrEmail.AutoSize = true;
            this.ErrEmail.ForeColor = System.Drawing.Color.Red;
            this.ErrEmail.Location = new System.Drawing.Point(525, 94);
            this.ErrEmail.Name = "ErrEmail";
            this.ErrEmail.Size = new System.Drawing.Size(13, 17);
            this.ErrEmail.TabIndex = 8;
            this.ErrEmail.Text = "*";
            // 
            // ErrPassword
            // 
            this.ErrPassword.AutoSize = true;
            this.ErrPassword.ForeColor = System.Drawing.Color.Red;
            this.ErrPassword.Location = new System.Drawing.Point(525, 139);
            this.ErrPassword.Name = "ErrPassword";
            this.ErrPassword.Size = new System.Drawing.Size(13, 17);
            this.ErrPassword.TabIndex = 9;
            this.ErrPassword.Text = "*";
            // 
            // StatusTitle
            // 
            this.StatusTitle.AutoSize = true;
            this.StatusTitle.Location = new System.Drawing.Point(290, 257);
            this.StatusTitle.Name = "StatusTitle";
            this.StatusTitle.Size = new System.Drawing.Size(98, 17);
            this.StatusTitle.TabIndex = 10;
            this.StatusTitle.Text = "Server Status:";
            // 
            // StatusText
            // 
            this.StatusText.AutoSize = true;
            this.StatusText.Location = new System.Drawing.Point(290, 274);
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(13, 17);
            this.StatusText.TabIndex = 11;
            this.StatusText.Text = "*";
            // 
            // RememberMe
            // 
            this.RememberMe.AutoSize = true;
            this.RememberMe.Location = new System.Drawing.Point(293, 224);
            this.RememberMe.Name = "RememberMe";
            this.RememberMe.Size = new System.Drawing.Size(122, 21);
            this.RememberMe.TabIndex = 12;
            this.RememberMe.Text = "Remember me";
            this.RememberMe.UseVisualStyleBackColor = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RememberMe);
            this.Controls.Add(this.StatusText);
            this.Controls.Add(this.StatusTitle);
            this.Controls.Add(this.ErrPassword);
            this.Controls.Add(this.ErrEmail);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textEmail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textEmail;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label ErrPassword;
        private System.Windows.Forms.Label StatusTitle;
        private System.Windows.Forms.Label StatusText;
        public System.Windows.Forms.Label ErrEmail;
        private System.Windows.Forms.CheckBox RememberMe;
    }
}