using DVLD_Business;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmChangePassword : Form
    {
        private int _UserID;
        private clsUser _User;
        public frmChangePassword(int userID)
        {
            InitializeComponent();
            _UserID = userID;
        }
        private void _ResetDefualtValues()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";

        }
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            _User = clsUser.FindByUserID(_UserID);
            if (_User == null)
            {// Here we don't continue because invalid user info.
                MessageBox.Show("Could not find user with id = " + _UserID, "ID not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctlUserCard1.LoadUserInfo(_UserID);

        }
        private void txtCurrentPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (_User.Password.Trim() != txtCurrentPassword.Text.Trim())
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, "Please enter matched password");
            }
            else
            {

                errorProvider1.SetError(txtCurrentPassword, "");
            }

        }
        private void txtNewPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtNewPassword.Text.Trim() == string.Empty)
            {
                e.Cancel = false;

                errorProvider1.SetError(txtConfirmPassword, "Please enter matched password");
            }
            else
            {

                errorProvider1.SetError(txtConfirmPassword, null);

            }
        }
        private void txtConfirmPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim() || txtConfirmPassword.Text.Trim() == string.Empty)
            {
                e.Cancel = false;

                errorProvider1.SetError(txtConfirmPassword, "Please enter matched password");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");

            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!, put the mouse on red icon to know the problem", "Not valid Inputs", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (clsUser.ChangePassword(_UserID, txtNewPassword.Text))
            {
                MessageBox.Show("User password changed successfully", "Passowrd Changed", MessageBoxButtons.OK);
                _ResetDefualtValues();

            }
            else
            {
                MessageBox.Show("Error: Password did not changed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
