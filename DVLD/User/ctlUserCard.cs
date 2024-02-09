using DVLD_Business;
using System.Windows.Forms;

namespace DVLD.User
{
    public partial class ctlUserCard : UserControl
    {
        private clsUser _User;
        private int _UserID = -1;
        public ctlUserCard()
        {
            InitializeComponent();
        }
        public void LoadUserInfo(int UserID)
        {
            _UserID = UserID;
            _User = clsUser.FindByUserID(_UserID);
            if (_User == null)
            {
                _ResetUserInfo();
                MessageBox.Show("No User with UserID = " + _UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillUserInfo();
        }
        private void _FillUserInfo()
        {
            ctlPersonCard1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();
            if (_User.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";
        }
        private void _ResetUserInfo()
        {
            ctlPersonCard1.ResetPersonInfo();
            lblIsActive.Text = "????";
            lblUserName.Text = "????";
            lblUserID.Text = "????";
        }

    }
}
