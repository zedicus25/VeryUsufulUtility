namespace GoogleChromeSpy
{
    partial class KeyInputForm
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
            this.keyTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // keyTB
            // 
            this.keyTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.keyTB.Location = new System.Drawing.Point(17, 87);
            this.keyTB.Name = "keyTB";
            this.keyTB.PasswordChar = '*';
            this.keyTB.Size = new System.Drawing.Size(254, 23);
            this.keyTB.TabIndex = 0;
            this.keyTB.TextChanged += new System.EventHandler(this.keyTB_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 75);
            this.label1.TabIndex = 1;
            this.label1.Text = "Your computer is HACKED!!\nSalaries to 1000$ aliens \nOr enter password ;)";
            // 
            // KeyInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 125);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.keyTB);
            this.Name = "KeyInputForm";
            this.Text = "KeyInputForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox keyTB;
        private System.Windows.Forms.Label label1;
    }
}