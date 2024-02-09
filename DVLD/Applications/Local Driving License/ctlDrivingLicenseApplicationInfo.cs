using DVLD_Business;
using System.Windows.Forms;
namespace DVLD
{
    public partial class ctlDrivingLicenseApplicationInfo : UserControl
    {
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _LicenseID = -1;
        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }
        public ctlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void _ResetLocalDrivinglicenseApplicationInfo()
        {
            _LocalDrivingLicenseApplicationID = -1;

            ctlApplicationBasicInfo.ResetApplicationBasicInfo();
            lblDLApplicationID.Text = "[???]";
            lblLicenseClass.Text = "[???]";
            lblPassedTest.Text = "0";
        }
        private void _FillLocalDrivinglicenseApplicationInfo()
        {
            _LicenseID = clsLicense.GetActiveLicenseIdByPersonID(_LocalDrivingLicenseApplication.PersonID, _LocalDrivingLicenseApplication.LicenseClassID);
            llShowLicenseInfo.Enabled = (_LicenseID != -1);
            lblLicenseClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblPassedTest.Text = _LocalDrivingLicenseApplication.GetPassedTestCount().ToString() + "/3";
            lblDLApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            ctlApplicationBasicInfo.LoadApplicationBasicInfo(_LocalDrivingLicenseApplication.ApplicationID);
        }
        public void LoadApplicationInfoByLocalDrivingAppID(int LocalDrivingLicenseApplicationID)
        {

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByID(LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetLocalDrivinglicenseApplicationInfo();
                MessageBox.Show("No Application With Application ID = " + LocalDrivingLicenseApplicationID, "No App Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _FillLocalDrivinglicenseApplicationInfo();
        }
        public void LoadApplicationInfoByApplicationID(int ApplicationID)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetLocalDrivinglicenseApplicationInfo();
                MessageBox.Show("No Application With Application ID = " + LocalDrivingLicenseApplicationID, "No App Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _FillLocalDrivinglicenseApplicationInfo();
        }
    }
}
