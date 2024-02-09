using DVLD.Global_Classes;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmDetainLicenseApplication : Form
    {
        private int _SelectedLicenseID = -1;
        private int _DetainID = -1;

        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }
        public frmDetainLicenseApplication(int LicenseID)
        {
            InitializeComponent();
            this._SelectedLicenseID = LicenseID;
            ctlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_SelectedLicenseID);
            ctlDriverLicenseInfoWithFilter1.FilterEnabled = false;
        }

        private void ctlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;
            lblLicenseID.Text = _SelectedLicenseID.ToString();

            llShowLicensesHistory.Enabled = (_SelectedLicenseID != -1);


            if (_SelectedLicenseID == -1)
            {
                return;
            }

            //Make sure lincese is not detained.
            if (ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License already detained!, choose another one", "License Detained", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            txtFineFees.Focus();
            btnDetainLicense.Enabled = true;
        }

        private void txtFineFees_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Please Enter The Fees");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFineFees, null);
            }
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to Detain This License With ID = " + _SelectedLicenseID, "Detaining License", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            _DetainID = ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Detain(Convert.ToDecimal(txtFineFees.Text), clsGlobal.CurrentUser.UserID);

            if (_DetainID == -1)
            {
                MessageBox.Show("Failed to Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblDetainID.Text = _DetainID.ToString();
            lblDetainDate.Text = ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainDate.ToShortDateString();
            MessageBox.Show("License Detained Successfully! with ID = " + _DetainID, "License Detained", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnDetainLicense.Enabled = false;
            ctlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            txtFineFees.Enabled = false;

            llShowLicenseInfo.Enabled = true;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }
    }
}
