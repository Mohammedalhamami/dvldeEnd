using DVLD.Global_Classes;
using DVLD_Business;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmLogin : Form
    {
        private static byte LoginTrials = 0;
        public frmLogin()
        {

            InitializeComponent();

        }


        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";

            if (clsGlobal.GetStoredCredential(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser User = clsUser.FindByUserNameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
            if (User != null)
            {
                if (chkRememberMe.Checked)
                {
                    //store logedin username and password in public static class.
                    clsGlobal.RememberUserNameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }

                if (!User.IsActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your account is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                clsGlobal.CurrentUser = User;
                this.Hide();
                //to make signout later on.
                frmMain frm = new frmMain(this);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid UserName/Password.", "Wrong credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //we give user 3 trials to login, after that system will close.
                if ((LoginTrials += 1) == 3)
                {
                    if (!EventLog.SourceExists("dvldEnd"))
                    {
                        EventLog.CreateEventSource("dvldEnd", "Application");
                    }
                    EventLog.WriteEntry("dvldEnd", "more than three times trial to log in", EventLogEntryType.Warning);

                    this.Close();
                }
                txtUserName.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
