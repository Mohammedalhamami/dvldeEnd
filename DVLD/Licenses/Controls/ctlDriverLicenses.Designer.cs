namespace DVLD
{
    partial class ctlDriverLicenses
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlDriverLicenses));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TCDriverLicenses = new System.Windows.Forms.TabControl();
            this.TP_Local = new System.Windows.Forms.TabPage();
            this.lblLocalRecordsCount = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dgvLocalLicenses = new System.Windows.Forms.DataGridView();
            this.cmLocalLicenseHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmshowLocalLicenseInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNext = new System.Windows.Forms.Button();
            this.TP_International = new System.Windows.Forms.TabPage();
            this.lblInternationalRecordsCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvInternationalLicenses = new System.Windows.Forms.DataGridView();
            this.cmInternationalLicenseHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmshowInternationalLicenseInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.TCDriverLicenses.SuspendLayout();
            this.TP_Local.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicenses)).BeginInit();
            this.cmLocalLicenseHistory.SuspendLayout();
            this.TP_International.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicenses)).BeginInit();
            this.cmInternationalLicenseHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.TCDriverLicenses);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(935, 236);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driver Licenses";
            // 
            // TCDriverLicenses
            // 
            this.TCDriverLicenses.Controls.Add(this.TP_Local);
            this.TCDriverLicenses.Controls.Add(this.TP_International);
            this.TCDriverLicenses.Location = new System.Drawing.Point(5, 19);
            this.TCDriverLicenses.Name = "TCDriverLicenses";
            this.TCDriverLicenses.SelectedIndex = 0;
            this.TCDriverLicenses.Size = new System.Drawing.Size(924, 211);
            this.TCDriverLicenses.TabIndex = 3;
            // 
            // TP_Local
            // 
            this.TP_Local.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TP_Local.Controls.Add(this.lblLocalRecordsCount);
            this.TP_Local.Controls.Add(this.label11);
            this.TP_Local.Controls.Add(this.label10);
            this.TP_Local.Controls.Add(this.label9);
            this.TP_Local.Controls.Add(this.dgvLocalLicenses);
            this.TP_Local.Controls.Add(this.btnNext);
            this.TP_Local.Location = new System.Drawing.Point(4, 22);
            this.TP_Local.Name = "TP_Local";
            this.TP_Local.Padding = new System.Windows.Forms.Padding(3);
            this.TP_Local.Size = new System.Drawing.Size(916, 185);
            this.TP_Local.TabIndex = 0;
            this.TP_Local.Text = "Local";
            this.TP_Local.UseVisualStyleBackColor = true;
            // 
            // lblLocalRecordsCount
            // 
            this.lblLocalRecordsCount.AutoSize = true;
            this.lblLocalRecordsCount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalRecordsCount.Location = new System.Drawing.Point(92, 164);
            this.lblLocalRecordsCount.Name = "lblLocalRecordsCount";
            this.lblLocalRecordsCount.Size = new System.Drawing.Size(19, 16);
            this.lblLocalRecordsCount.TabIndex = 32;
            this.lblLocalRecordsCount.Text = "??";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(10, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(155, 16);
            this.label11.TabIndex = 11;
            this.label11.Text = "Local Licenses History:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 164);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 16);
            this.label10.TabIndex = 10;
            this.label10.Text = "#Records:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(-496, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 19);
            this.label9.TabIndex = 9;
            this.label9.Text = "#Records:";
            // 
            // dgvLocalLicenses
            // 
            this.dgvLocalLicenses.AllowUserToAddRows = false;
            this.dgvLocalLicenses.AllowUserToDeleteRows = false;
            this.dgvLocalLicenses.BackgroundColor = System.Drawing.Color.White;
            this.dgvLocalLicenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalLicenses.ContextMenuStrip = this.cmLocalLicenseHistory;
            this.dgvLocalLicenses.Location = new System.Drawing.Point(13, 31);
            this.dgvLocalLicenses.Name = "dgvLocalLicenses";
            this.dgvLocalLicenses.ReadOnly = true;
            this.dgvLocalLicenses.Size = new System.Drawing.Size(895, 130);
            this.dgvLocalLicenses.TabIndex = 8;
            // 
            // cmLocalLicenseHistory
            // 
            this.cmLocalLicenseHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmshowLocalLicenseInfo});
            this.cmLocalLicenseHistory.Name = "cmLocalLicenseHistory";
            this.cmLocalLicenseHistory.Size = new System.Drawing.Size(186, 42);
            // 
            // tsmshowLocalLicenseInfo
            // 
            this.tsmshowLocalLicenseInfo.Image = global::DVLD.Properties.Resources.License_View_32;
            this.tsmshowLocalLicenseInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmshowLocalLicenseInfo.Name = "tsmshowLocalLicenseInfo";
            this.tsmshowLocalLicenseInfo.Size = new System.Drawing.Size(185, 38);
            this.tsmshowLocalLicenseInfo.Text = "Show License Info";
            this.tsmshowLocalLicenseInfo.Click += new System.EventHandler(this.tsmshowLocalLicenseInfo_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.White;
            this.btnNext.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(619, 343);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(106, 32);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            // 
            // TP_International
            // 
            this.TP_International.Controls.Add(this.lblInternationalRecordsCount);
            this.TP_International.Controls.Add(this.label1);
            this.TP_International.Controls.Add(this.label2);
            this.TP_International.Controls.Add(this.dgvInternationalLicenses);
            this.TP_International.Location = new System.Drawing.Point(4, 22);
            this.TP_International.Name = "TP_International";
            this.TP_International.Padding = new System.Windows.Forms.Padding(3);
            this.TP_International.Size = new System.Drawing.Size(916, 185);
            this.TP_International.TabIndex = 1;
            this.TP_International.Text = "International";
            this.TP_International.UseVisualStyleBackColor = true;
            // 
            // lblInternationalRecordsCount
            // 
            this.lblInternationalRecordsCount.AutoSize = true;
            this.lblInternationalRecordsCount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInternationalRecordsCount.Location = new System.Drawing.Point(92, 166);
            this.lblInternationalRecordsCount.Name = "lblInternationalRecordsCount";
            this.lblInternationalRecordsCount.Size = new System.Drawing.Size(19, 16);
            this.lblInternationalRecordsCount.TabIndex = 31;
            this.lblInternationalRecordsCount.Text = "??";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "International Licenses History:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "#Records:";
            // 
            // dgvInternationalLicenses
            // 
            this.dgvInternationalLicenses.AllowUserToAddRows = false;
            this.dgvInternationalLicenses.AllowUserToDeleteRows = false;
            this.dgvInternationalLicenses.BackgroundColor = System.Drawing.Color.White;
            this.dgvInternationalLicenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInternationalLicenses.ContextMenuStrip = this.cmInternationalLicenseHistory;
            this.dgvInternationalLicenses.Location = new System.Drawing.Point(15, 33);
            this.dgvInternationalLicenses.Name = "dgvInternationalLicenses";
            this.dgvInternationalLicenses.ReadOnly = true;
            this.dgvInternationalLicenses.Size = new System.Drawing.Size(895, 130);
            this.dgvInternationalLicenses.TabIndex = 12;
            // 
            // cmInternationalLicenseHistory
            // 
            this.cmInternationalLicenseHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmshowInternationalLicenseInfo});
            this.cmInternationalLicenseHistory.Name = "cmInternationalLicenseHistory";
            this.cmInternationalLicenseHistory.Size = new System.Drawing.Size(186, 42);
            // 
            // tsmshowInternationalLicenseInfo
            // 
            this.tsmshowInternationalLicenseInfo.Image = global::DVLD.Properties.Resources.License_View_32;
            this.tsmshowInternationalLicenseInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmshowInternationalLicenseInfo.Name = "tsmshowInternationalLicenseInfo";
            this.tsmshowInternationalLicenseInfo.Size = new System.Drawing.Size(185, 38);
            this.tsmshowInternationalLicenseInfo.Text = "Show License Info";
            this.tsmshowInternationalLicenseInfo.Click += new System.EventHandler(this.tsmshowInternationalLicenseInfo_Click);
            // 
            // ctlDriverLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.Name = "ctlDriverLicenses";
            this.Size = new System.Drawing.Size(945, 242);
            this.groupBox1.ResumeLayout(false);
            this.TCDriverLicenses.ResumeLayout(false);
            this.TP_Local.ResumeLayout(false);
            this.TP_Local.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicenses)).EndInit();
            this.cmLocalLicenseHistory.ResumeLayout(false);
            this.TP_International.ResumeLayout(false);
            this.TP_International.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicenses)).EndInit();
            this.cmInternationalLicenseHistory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl TCDriverLicenses;
        private System.Windows.Forms.TabPage TP_Local;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dgvLocalLicenses;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TabPage TP_International;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvInternationalLicenses;
         private System.Windows.Forms.Label lblInternationalRecordsCount;
        private System.Windows.Forms.Label lblLocalRecordsCount;
        private System.Windows.Forms.ContextMenuStrip cmLocalLicenseHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmshowLocalLicenseInfo;
        private System.Windows.Forms.ContextMenuStrip cmInternationalLicenseHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmshowInternationalLicenseInfo;
    }
}
