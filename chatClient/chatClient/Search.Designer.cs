namespace chatClient
{
    partial class Search
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
            System.Windows.Forms.Button buttonSearch;
            this.label1 = new System.Windows.Forms.Label();
            this.textNumber = new System.Windows.Forms.TextBox();
            this.InputLabel = new System.Windows.Forms.Label();
            buttonSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(82, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Search";
            // 
            // textNumber
            // 
            this.textNumber.Location = new System.Drawing.Point(90, 114);
            this.textNumber.Name = "textNumber";
            this.textNumber.Size = new System.Drawing.Size(221, 22);
            this.textNumber.TabIndex = 1;
            // 
            // InputLabel
            // 
            this.InputLabel.AutoSize = true;
            this.InputLabel.Location = new System.Drawing.Point(87, 94);
            this.InputLabel.Name = "InputLabel";
            this.InputLabel.Size = new System.Drawing.Size(95, 17);
            this.InputLabel.TabIndex = 2;
            this.InputLabel.Text = "Input number:";
            // 
            // buttonSearch
            // 
            buttonSearch.Location = new System.Drawing.Point(90, 142);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new System.Drawing.Size(221, 38);
            buttonSearch.TabIndex = 3;
            buttonSearch.Text = "Search";
            buttonSearch.UseVisualStyleBackColor = true;
            buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 226);
            this.Controls.Add(buttonSearch);
            this.Controls.Add(this.InputLabel);
            this.Controls.Add(this.textNumber);
            this.Controls.Add(this.label1);
            this.Name = "Search";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textNumber;
        private System.Windows.Forms.Label InputLabel;
    }
}