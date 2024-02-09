namespace DVLD.People
{
    partial class frmShowPersonInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowPersonInfo));
            this.label1 = new System.Windows.Forms.Label();
            this.ctlPersonCard1 = new DVLD.Controls.ctlPersonCard();
            this.btnPersonDetailsClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Felix Titling", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(266, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Person Detials";
            // 
            // ctlPersonCard1
            // 
            this.ctlPersonCard1.BackColor = System.Drawing.Color.White;
            this.ctlPersonCard1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlPersonCard1.Location = new System.Drawing.Point(13, 88);
            this.ctlPersonCard1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctlPersonCard1.Name = "ctlPersonCard1";
            this.ctlPersonCard1.Size = new System.Drawing.Size(765, 257);
            this.ctlPersonCard1.TabIndex = 3;
            // 
            // btnPersonDetailsClose
            // 
            this.btnPersonDetailsClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPersonDetailsClose.Image = ((System.Drawing.Image)(resources.GetObject("btnPersonDetailsClose.Image")));
            this.btnPersonDetailsClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPersonDetailsClose.Location = new System.Drawing.Point(683, 353);
            this.btnPersonDetailsClose.Name = "btnPersonDetailsClose";
            this.btnPersonDetailsClose.Size = new System.Drawing.Size(95, 32);
            this.btnPersonDetailsClose.TabIndex = 26;
            this.btnPersonDetailsClose.Text = "Close";
            this.btnPersonDetailsClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPersonDetailsClose.UseVisualStyleBackColor = true;
            this.btnPersonDetailsClose.Click += new System.EventHandler(this.btnPersonDetailsClose_Click);
            // 
            // frmShowPersonInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 397);
            this.Controls.Add(this.btnPersonDetailsClose);
            this.Controls.Add(this.ctlPersonCard1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowPersonInfo";
            this.Text = "PersonDetials";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Controls.ctlPersonCard ctlPersonCard1;
        private System.Windows.Forms.Button btnPersonDetailsClose;
    }
}