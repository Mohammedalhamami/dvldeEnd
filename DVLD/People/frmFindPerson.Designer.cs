namespace DVLD
{
    partial class frmFindPerson
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFindPerson));
            this.label1 = new System.Windows.Forms.Label();
            this.btnPersonDetailsClose = new System.Windows.Forms.Button();
            this.ctlPersonCardWithFilter1 = new DVLD.Controls.ctlPersonCardWithFilter();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Felix Titling", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(287, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Find Person";
            // 
            // btnPersonDetailsClose
            // 
            this.btnPersonDetailsClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPersonDetailsClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPersonDetailsClose.Image = ((System.Drawing.Image)(resources.GetObject("btnPersonDetailsClose.Image")));
            this.btnPersonDetailsClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPersonDetailsClose.Location = new System.Drawing.Point(682, 407);
            this.btnPersonDetailsClose.Name = "btnPersonDetailsClose";
            this.btnPersonDetailsClose.Size = new System.Drawing.Size(95, 32);
            this.btnPersonDetailsClose.TabIndex = 25;
            this.btnPersonDetailsClose.Text = "Close";
            this.btnPersonDetailsClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPersonDetailsClose.UseVisualStyleBackColor = true;
            this.btnPersonDetailsClose.Click += new System.EventHandler(this.btnPersonDetailsClose_Click);
            // 
            // ctlPersonCardWithFilter1
            // 
            this.ctlPersonCardWithFilter1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ctlPersonCardWithFilter1.BackColor = System.Drawing.Color.White;
            this.ctlPersonCardWithFilter1.FilterEnabled = true;
            this.ctlPersonCardWithFilter1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlPersonCardWithFilter1.Location = new System.Drawing.Point(13, 62);
            this.ctlPersonCardWithFilter1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctlPersonCardWithFilter1.Name = "ctlPersonCardWithFilter1";
            this.ctlPersonCardWithFilter1.ShowAddPerson = true;
            this.ctlPersonCardWithFilter1.Size = new System.Drawing.Size(768, 337);
            this.ctlPersonCardWithFilter1.TabIndex = 26;
            // 
            // frmFindPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(789, 450);
            this.Controls.Add(this.ctlPersonCardWithFilter1);
            this.Controls.Add(this.btnPersonDetailsClose);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFindPerson";
            this.Text = "PersonDetails";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPersonDetailsClose;
        private Controls.ctlPersonCardWithFilter ctlPersonCardWithFilter1;
    }
}