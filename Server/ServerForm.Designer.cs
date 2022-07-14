namespace Server
{
    partial class ServerForm
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
            this.usersLB = new System.Windows.Forms.ListBox();
            this.getInfoBtn = new System.Windows.Forms.Button();
            this.getVersBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // usersLB
            // 
            this.usersLB.FormattingEnabled = true;
            this.usersLB.Location = new System.Drawing.Point(12, 12);
            this.usersLB.Name = "usersLB";
            this.usersLB.Size = new System.Drawing.Size(291, 264);
            this.usersLB.TabIndex = 0;
            // 
            // getInfoBtn
            // 
            this.getInfoBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.getInfoBtn.Location = new System.Drawing.Point(12, 291);
            this.getInfoBtn.Name = "getInfoBtn";
            this.getInfoBtn.Size = new System.Drawing.Size(121, 56);
            this.getInfoBtn.TabIndex = 1;
            this.getInfoBtn.Text = "Get history";
            this.getInfoBtn.UseVisualStyleBackColor = true;
            this.getInfoBtn.Click += new System.EventHandler(this.getInfoBtn_Click);
            // 
            // getVersBtn
            // 
            this.getVersBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.getVersBtn.Location = new System.Drawing.Point(182, 291);
            this.getVersBtn.Name = "getVersBtn";
            this.getVersBtn.Size = new System.Drawing.Size(121, 56);
            this.getVersBtn.TabIndex = 2;
            this.getVersBtn.Text = "Get version";
            this.getVersBtn.UseVisualStyleBackColor = true;
            this.getVersBtn.Click += new System.EventHandler(this.getVersBtn_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 363);
            this.Controls.Add(this.getVersBtn);
            this.Controls.Add(this.getInfoBtn);
            this.Controls.Add(this.usersLB);
            this.Name = "ServerForm";
            this.Text = "Server";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox usersLB;
        private System.Windows.Forms.Button getInfoBtn;
        private System.Windows.Forms.Button getVersBtn;
    }
}

