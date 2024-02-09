using DVLD.Global_Classes;
using DVLD_Business;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmEditApplicationTypes : Form
    {

        private clsApplicationType _ApplicationType;
        public frmEditApplicationTypes(int ID)
        {
            InitializeComponent();
            _ApplicationType = clsApplicationType.Find(ID);
            if (_ApplicationType != null)
            {
                lblID.Text = ID.ToString();
                txtTitle.Text = _ApplicationType.ApplicationTypeTitle;
                txtFees.Text = _ApplicationType.ApplicationFees.ToString();
            }
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!", "Invalid Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _ApplicationType.ApplicationTypeTitle = txtTitle.Text;
            _ApplicationType.ApplicationFees = Convert.ToDecimal(txtFees.Text.Trim());
            if (_ApplicationType.Save())
            {
                MessageBox.Show("Data Saved Successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data is not saved successfully!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void txtTitle_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                txtTitle.Focus();
                errorProvider1.SetError(txtTitle, "Enter Application Title");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtFees_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!clsValidation.IsNumber(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                txtFees.Focus();
                errorProvider1.SetError(txtFees, "Enter Valid Value");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFees, null);
            }


        }

        private void frmEditApplicationTypes_Load(object sender, EventArgs e)
        {

        }
    }
}
