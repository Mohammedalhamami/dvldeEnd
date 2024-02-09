using System.Windows.Forms;

namespace DVLD
{
    public partial class frmShowInternationalLicenseInfo : Form
    {
        public frmShowInternationalLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            ctlDriverInternationalLicenseInfo1.LoadInfo(LicenseID);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
