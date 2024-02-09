using DVLD.Global_Classes;
using DVLD_Business;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmReleaseDetainedDrivingLicense : Form
    {
        private int _SelectedLicenseID = -1;
        // private int _DetainLicenseID = -1;
        public frmReleaseDetainedDrivingLicense()
        {
            InitializeComponent();
        }
        public frmReleaseDetainedDrivingLicense(int LicenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = LicenseID;
            ctlDriverLicenseInfoWithFilter2.LoadLicenseInfo(_SelectedLicenseID);
            ctlDriverLicenseInfoWithFilter2.FilterEnabled = false;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctlDriverLicenseInfoWithFilter2.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }





        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Release Detained License With ID = " + _SelectedLicenseID, "Detaining License", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ApplicationID = -1;

            bool IsReleased = ctlDriverLicenseInfoWithFilter2.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID);

            lblApplicationID.Text = ApplicationID.ToString();
            if (!IsReleased)
            {
                MessageBox.Show("Faild to Release Detained License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Detained License Released Successfully", "License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            llShowLicenseInfo.Enabled = true;
            ctlDriverLicenseInfoWithFilter2.FilterEnabled = false;
            btnReleaseDetainedLicense.Enabled = false;



        }

        private void ctlDriverLicenseInfoWithFilter2_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            if (_SelectedLicenseID == -1)
            {
                return;
            }

            lblLicenseID.Text = _SelectedLicenseID.ToString();

            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            //Make sure License is Detained
            if (!ctlDriverLicenseInfoWithFilter2.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License is not detained!, choose another one", "License is not Detained", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            btnReleaseDetainedLicense.Enabled = true;
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicense).ApplicationFees.ToString();

            lblDetainID.Text = ctlDriverLicenseInfoWithFilter2.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            lblDetainDate.Text = ctlDriverLicenseInfoWithFilter2.SelectedLicenseInfo.DetainedInfo.DetainDate.ToShortDateString();
            lblCreatedByUserName.Text = ctlDriverLicenseInfoWithFilter2.SelectedLicenseInfo.DetainedInfo.CreatedByUserInfo.UserName;
            lblFineFees.Text = ctlDriverLicenseInfoWithFilter2.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToDecimal(lblApplicationFees.Text) + Convert.ToDecimal(lblFineFees.Text)).ToString();

        }
    }
}
