using DVLD_Business;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmAddEditUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;
        private int _UserID = -1;
        private clsUser _User;


        public frmAddEditUser()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }
        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            lblUserTitle.Text = "Edit User";
            _Mode = enMode.Update;
            _UserID = UserID;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {

                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                TCAddUser.SelectedTab = TCAddUser.TabPages["tpLoginInfo"];

                return;
            }
            //in case of addNew mode
            if (ctlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsUser.IsUserExistByPersonID(ctlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already is a user, choose another one.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ctlPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    TCAddUser.SelectedTab = TCAddUser.TabPages["tpLoginInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please Select a person", "Select a person", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ctlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnAddEditPersonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _ResetDefualtValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblUserTitle.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsUser();
                tpLoginInfo.Enabled = false;
                ctlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                lblUserTitle.Text = "Update User";
                this.Text = "Update User";

                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
        }
        private void _LoadData()
        {
            _User = clsUser.FindByUserID(_UserID);
            ctlPersonCardWithFilter1.FilterEnabled = false;
            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _UserID, "User is Not Found", MessageBoxButtons.OK);
                this.Close();
                return;
            }

            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName.ToString();
            txtPassword.Text = _User.Password.ToString();
            chkIsActive.Checked = _User.IsActive;
            ctlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);

        }
        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid, put the mouse over the red icon", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _User.PersonID = ctlPersonCardWithFilter1.PersonID;
            _User.UserName = txtUserName.Text;
            _User.Password = txtPassword.Text;
            _User.IsActive = chkIsActive.Checked;

            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                _Mode = enMode.Update;
                lblUserTitle.Text = "Update User";
                this.Text = "Update User";

                MessageBox.Show("Data saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data is not saved Successfully.", "Not saved", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void txtUserName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtUserName.Text.Trim() == string.Empty)
            {
                e.Cancel = true;
                txtUserName.Focus();
                errorProvider1.SetError(txtUserName, "Please Enter username");
            }
            else if (clsUser.IsUserExist(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                txtUserName.Focus();
                errorProvider1.SetError(txtUserName, "User name is used by another user");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, "");
            }
        }

        private void txtPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (txtPassword.Text.Trim() == string.Empty)
            {
                e.Cancel = true;
                txtPassword.Focus();
                errorProvider1.SetError(txtPassword, "Please Enter password");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim() || txtConfirmPassword.Text.Trim() == string.Empty)
            {
                e.Cancel = true;
                txtConfirmPassword.Focus();
                errorProvider1.SetError(txtConfirmPassword, "password should match");
                txtConfirmPassword.Text = string.Empty;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }
    }
}
