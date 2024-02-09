using DVLD.Global_Classes;
using DVLD_Business;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmReNewLocalDrivingLicenseApplication : Form
    {
        private int _NewLicenseID = -1;
        public frmReNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }




        private void frmReNewLocalDrivingLicenseApplication_Load(object sender, System.EventArgs e)
        {
            ctlDriverLicenseInfoWithFilter1.txtLicenseIdFocus();

            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = "????";
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees.ToString();

            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;

        }

        private void ctlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            llblShowLicensesHistory.Enabled = (SelectedLicenseID != -1);
            if (SelectedLicenseID == -1)
            {
                return;
            }

            byte DefaultValidityLength = ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassInfo.DefaultValidityLength;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(DefaultValidityLength));
            lblLicenseFees.Text = ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassInfo.ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToDecimal(lblApplicationFees.Text) + Convert.ToDecimal(lblLicenseFees.Text)).ToString();
            txtNotes.Text = ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Notes;

            //check if license is not expired.
            if (!ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("Selected License is not yet Expired, it will expire on : " + ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ExpirationDate,
                      "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                return;
            }

            //check if license is not Active
            if (!ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected license is not active, choose another license.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                return;
            }
            btnRenew.Enabled = true;
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsLicense NewLicense = ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.RenewLicense(txtNotes.Text, clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            llblShowNewLicensesInfo.Enabled = true;
            txtNotes.ReadOnly = true;

            _NewLicenseID = NewLicense.LicenseID;

            lblRenewedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("License Renewed Successfully with ID= " + _NewLicenseID, "License Renewed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            btnRenew.Enabled = false;
            ctlDriverLicenseInfoWithFilter1.FilterEnabled = false;

        }

        private void llblShowNewLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

