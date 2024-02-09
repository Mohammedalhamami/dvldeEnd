using DVLD.Global_Classes;
using DVLD.Properties;
using DVLD_Business;
using System.IO;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctlDriverInternationalLicenseInfo : UserControl
    {
        private int _InternationalLicenseID = -1;
        private clsInternationalLicense _InternationalLicense;
        public clsInternationalLicense SelectedInternationalLicenseInfo
        {
            get { return _InternationalLicense; }
        }

        public int InternationalLicenseID
        {
            get { return _InternationalLicenseID; }

        }


        public ctlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private void _LoadPersonImage()
        {
            if (_InternationalLicense.DriverInfo.PersonInfo.Gender == 0)
                pbDriverImg.Image = Resources.Male_512;
            else
                pbDriverImg.Image = Resources.Female_512;

            string ImagePath = _InternationalLicense.DriverInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbDriverImg.Load(ImagePath);
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void LoadInfo(int LicenseID)
        {
            _InternationalLicenseID = LicenseID;
            _InternationalLicense = clsInternationalLicense.Find(_InternationalLicenseID);

            if (_InternationalLicense == null)
            {
                MessageBox.Show("Could not find International License with ID = " + _InternationalLicenseID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _InternationalLicenseID = -1;
                return;
            }

            lblDriverName.Text = _InternationalLicense.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = _InternationalLicenseID.ToString();
            lblNationalNo.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = (_InternationalLicense.DriverInfo.PersonInfo.Gender == 0) ? "Male" : "Female";
            lblIssueDate.Text = _InternationalLicense.IssueDate.ToShortDateString();
            lblInternationalLIcenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblDateOfBirth.Text = _InternationalLicense.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblIsActive.Text = _InternationalLicense.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = clsFormat.DateToShort(_InternationalLicense.DriverInfo.PersonInfo.DateOfBirth);
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(_InternationalLicense.ExpirationDate);
            _LoadPersonImage();

        }

        private void ctlDriverInternationalLicenseInfo_Load(object sender, System.EventArgs e)
        {
            if (!this.DesignMode)
            {
                //....stuff
            }
        }
    }
}
