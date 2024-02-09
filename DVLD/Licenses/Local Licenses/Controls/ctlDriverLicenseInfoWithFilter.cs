using DVLD.Global_Classes;
using DVLD_Business;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD.Licenses.Local_Licenses.Controls
{
    public partial class ctlDriverLicenseInfoWithFilter : UserControl
    {
        //Define Custom event handler.
        public event Action<int> OnLicenseSelected;
        //create a protected method to raise the event with a parameter.
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID); //Raise the event with the parameter.
            }
        }
        public ctlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }
        private int _LicenseID = -1;

        public int LicenseID
        {
            get { return ctlDriverLicenseInfo2.LicenseID; }
        }
        public clsLicense SelectedLicenseInfo
        {
            get { return ctlDriverLicenseInfo2.SelectedLicenseInfo; }
        }
        public void LoadLicenseInfo(int LicenseID)
        {
            txtLicenseID.Text = LicenseID.ToString();

            ctlDriverLicenseInfo2.LoadInfo(LicenseID);
            _LicenseID = ctlDriverLicenseInfo2.LicenseID;
            if (OnLicenseSelected != null && FilterEnabled)
            {
                //Raise the event with the parameter.
                OnLicenseSelected(_LicenseID);
            }

        }
        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (!clsValidation.IsNumber(txtLicenseID.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "Not valid value");
                txtLicenseID.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtLicenseID, null);

            }
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("The field is not valid, put the mouse over the red icon", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;
            }
            _LicenseID = int.Parse(txtLicenseID.Text);
            LoadLicenseInfo(_LicenseID);

        }
        public void txtLicenseIdFocus()
        {
            txtLicenseID.Focus();
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            //check if the passed key is Enter (character code 13)

            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
        }
    }

}
