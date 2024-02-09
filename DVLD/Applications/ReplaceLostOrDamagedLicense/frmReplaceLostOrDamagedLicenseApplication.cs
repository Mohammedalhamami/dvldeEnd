using DVLD.Global_Classes;
using DVLD_Business;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmReplaceLostOrDamagedLicenseApplication : Form
    {
        public enum enReplacementMode { Damaged = 3, Lost = 4 };
        private enReplacementMode Mode = enReplacementMode.Damaged;

        private int _NewLicenseID = -1;
        private int _GetApplicationTypeID()
        {
            if (rbDamagedLicense.Checked)
                return (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            else
                return (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
        }
        public frmReplaceLostOrDamagedLicenseApplication()
        {
            InitializeComponent();
        }

        private void rbDamaged_CheckedChanged(object sender, System.EventArgs e)
        {

            this.Text = "Replacement For Damaged License";
            lblTitle.Text = "Replacement For Damaged License";
            Mode = enReplacementMode.Damaged;
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).ApplicationFees.ToString();
        }

        private void rbLostLicense_CheckedChanged(object sender, System.EventArgs e)
        {
            this.Text = "Replacement for Lost License";
            lblTitle.Text = "Replacement For Lost License";
            Mode = enReplacementMode.Lost;
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).ApplicationFees.ToString();

        }

        private void frmReplaceLostOrDamagedLicenseApplication_Load(object sender, System.EventArgs e)
        {
            //rbDamagedLicense.Checked = true;
            //  Mode = enReplacementMode.Damaged;
            rbDamagedLicense.Checked = true;
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUserName.Text = clsGlobal.CurrentUser.UserName;

        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ctlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);
            if (SelectedLicenseID == -1)
            {
                return;
            }
            lblOldLicenseID.Text = SelectedLicenseID.ToString();

            //Do not allow replacement if license is not active
            if (!ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected license is not active, choose another license.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;
                return;
            }
            btnIssueReplacement.Enabled = true;
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Replace the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsLicense NewLicense = ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ReplaceDamagedOrLostLicense((clsLicense.enIssueReason)Mode, clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Replace the license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            llShowNewLicenseInfo.Enabled = true;


            _NewLicenseID = NewLicense.LicenseID;

            lblReplacedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("License Replaced Successfully with ID= " + _NewLicenseID, "License Replaced", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            btnIssueReplacement.Enabled = false;
            ctlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            gbReplacement.Enabled = false;

        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();

        }
    }
}
