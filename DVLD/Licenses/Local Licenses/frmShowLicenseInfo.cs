using System.Windows.Forms;

namespace DVLD
{
    public partial class frmShowLicenseInfo : Form
    {
        private int _LicenseID = -1;


        public frmShowLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void frmShowLicenseInfo_Load(object sender, System.EventArgs e)
        {

            ctlDrivingLicenseInfo1.LoadInfo(_LicenseID);
        }
    }
}
