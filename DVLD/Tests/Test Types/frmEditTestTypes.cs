using DVLD.Global_Classes;
using DVLD_Business;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmEditTestTypes : Form
    {

        private clsTestType _TestType;
        public frmEditTestTypes(int ID)
        {
            InitializeComponent();
            if ((_TestType = clsTestType.Find((clsTestType.enTestType)ID)) != null)
            {
                lblID.Text = ((int)_TestType.TestTypeID).ToString();
                lblTitle.Text = _TestType.TestTypeTitle.ToString();
                lblDescription.Text = _TestType.TestTypeDescription.ToString();
                lblFees.Text = _TestType.TestTypeFees.ToString();
            }
        }


        private void lblTitle_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(lblTitle.Text) || clsValidation.IsNumber(lblTitle.Text))
            {
                e.Cancel = true;
                lblTitle.Focus();
                errorProvider1.SetError(lblTitle, "Enter a valid value");
            }
            else
            {
                errorProvider1.SetError(lblTitle, null);
            }

        }

        private void lblDescription_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(lblDescription.Text))
            {
                e.Cancel = true;
                lblDescription.Focus();
                errorProvider1.SetError(lblDescription, "Enter a valid value");
            }
            else
            {
                errorProvider1.SetError(lblDescription, null);
            }
        }

        private void lblFees_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(lblFees.Text) || !clsValidation.IsNumber(lblFees.Text))
            {
                e.Cancel = true;
                lblFees.Focus();
                errorProvider1.SetError(lblFees, "Enter valid value");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Error: Some values not valid", "Invalid values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _TestType.TestTypeTitle = lblTitle.Text.Trim();
            _TestType.TestTypeDescription = lblDescription.Text.Trim();
            _TestType.TestTypeFees = Convert.ToDecimal(lblFees.Text.Trim());
            if (_TestType.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data is not saved successfully!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
