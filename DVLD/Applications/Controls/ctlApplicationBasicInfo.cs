using DVLD.People;
using DVLD_Business;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ApplicationBasicInfo : UserControl
    {
        private int _ApplicationID = -1;
        private clsApplication _ApplicatoinInfo;

        public ApplicationBasicInfo()
        {
            InitializeComponent();
        }
        public void ResetApplicationBasicInfo()
        {
            lblApplicationID.Text = "[???]";
            lblApplicant.Text = "[???]";
            lblStatus.Text = "[???]";
            lblFees.Text = "[$$$]";
            lblType.Text = "[???]";
            lblDate.Text = "??/??/????";
            lblStatusDate.Text = "??/??/????";
            lblCreatedBy.Text = "[???]";
        }
        private void _FillApplicationBasicInfo()
        {
            lblApplicationID.Text = _ApplicatoinInfo.ApplicationID.ToString();
            lblApplicant.Text = _ApplicatoinInfo.ApplicantFullName.ToString();
            lblStatus.Text = _ApplicatoinInfo.StatusText;
            lblFees.Text = _ApplicatoinInfo.PaidFees.ToString();
            lblType.Text = _ApplicatoinInfo.ApplicationTypeInfo.ApplicationTypeTitle.ToString();
            lblDate.Text = _ApplicatoinInfo.Date.ToShortDateString();
            lblStatusDate.Text = _ApplicatoinInfo.LastStatusDate.ToShortDateString();
            lblCreatedBy.Text = _ApplicatoinInfo.CreatedByUserInfo.UserName;
            llShowPersonInfo.Enabled = true;
        }

        public void LoadApplicationBasicInfo(int ApplicationID)
        {
            _ApplicatoinInfo = clsApplication.FindBaseApplication(ApplicationID);
            if (_ApplicatoinInfo == null)
            {
                ResetApplicationBasicInfo();
                MessageBox.Show("No Application With Application ID = " + ApplicationID, "No App Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _FillApplicationBasicInfo();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_ApplicatoinInfo.PersonID);
            frm.ShowDialog();
        }


    }
}
