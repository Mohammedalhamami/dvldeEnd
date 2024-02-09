using DVLD.Global_Classes;
using DVLD_Business;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmInternationalLicenseApplication : Form
    {
        private int _SelectedLicenseID = -1;
        private int _InternationalLicenseID = -1;
        public frmInternationalLicenseApplication(int LicenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = LicenseID;
            ctlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            ctlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_SelectedLicenseID);
        }
        public frmInternationalLicenseApplication()
        {
            InitializeComponent();
        }


        private void frmInternationalLicenseApplication_Load(object sender, System.EventArgs e)
        {

        }

        private void ctlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            if (_SelectedLicenseID == -1)
            {
                return;
            }
            lblLocalLicenseID.Text = _SelectedLicenseID.ToString();

            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            //Make sure License is Not International
            if (ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("Selected License should be Class 3, Choose another one", "Require License Class 3", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                llShowLicenseHistory.Enabled = true;
                return;
            }
            int ActiveInternationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);

            if (ActiveInternationalLicenseID != -1)
            {
                MessageBox.Show("Person has alreay Active International License, Choose another one", "has Active International License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = true;
                _InternationalLicenseID = ActiveInternationalLicenseID;
                btnIssue.Enabled = false;
                return;
            }

            btnIssue.Enabled = true;

        }

        private void btnIssue_Click(object sender, System.EventArgs e)
        {
            clsInternationalLicense InternationalLicense = new clsInternationalLicense();

            InternationalLicense.PersonID = ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.DriverID = ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID;
            InternationalLicense.Date = DateTime.Now;
            InternationalLicense.Status = clsApplication.enApplicationStatus.Complete;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctlDriverLicenseInfoWithFilter1.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Failed to Issue International License", "Issue Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblInternationalApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblApplicationDate.Text = InternationalLicense.Date.ToShortDateString();
            lblCreatedByUserName.Text = InternationalLicense.CreatedByUserInfo.UserName;
            lblIssueDate.Text = InternationalLicense.IssueDate.ToShortDateString();
            lblExpirationDate.Text = InternationalLicense.ExpirationDate.ToShortDateString();
            MessageBox.Show("International License Issued Successfully with ID" + _InternationalLicenseID, "Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnIssue.Enabled = false;
            ctlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(_InternationalLicenseID);
            frm.ShowDialog();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
