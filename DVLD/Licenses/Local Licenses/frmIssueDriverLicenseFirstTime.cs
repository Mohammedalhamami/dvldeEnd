using DVLD.Global_Classes;
using DVLD_Business;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmIssueDLFirstTime : Form
    {
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        public frmIssueDLFirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

        }
        private void frmIssueDLFirstTime_Load(object sender, System.EventArgs e)
        {

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByID(_LocalDrivingLicenseApplicationID);


            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (!_LocalDrivingLicenseApplication.PassedAllTests())
            {
                MessageBox.Show("Person Should pass all tests first.", "Not All Tests Passsed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
                return;
            }

            int LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();

            if (LicenseID != -1)
            {
                MessageBox.Show("Person Already has License before with ID = " + LicenseID, "Has License before", MessageBoxButtons.OK, MessageBoxIcon.Question);
                this.Close();
                return;
            }

            ctlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);

            txtNotes.Focus();
        }
        private void btnCloseManagePeople_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        private void btnIssue_Click(object sender, System.EventArgs e)
        {

            int LicenseID = _LocalDrivingLicenseApplication.IssueLicenseForTheFirstTime(txtNotes.Text.Trim(), clsGlobal.CurrentUser.PersonID);

            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with ID = " + LicenseID, "Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: Licenese Was not issued", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

    }
}

