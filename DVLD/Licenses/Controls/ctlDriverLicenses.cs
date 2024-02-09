using DVLD_Business;
using System.Data;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctlDriverLicenses : UserControl
    {
        private int _DriverID = -1;
        private clsDriver _Driver;
        private DataTable _dtDriverLocalLicenseHistory;
        private DataTable _dtDriverInternationalLicenseHistory;


        public ctlDriverLicenses()
        {
            InitializeComponent();
        }
        private void _LoadLocalLicenseInfo()
        {
            _dtDriverLocalLicenseHistory = clsLicense.GetDriverLicenses(_DriverID);

            dgvLocalLicenses.DataSource = _dtDriverLocalLicenseHistory;
            lblLocalRecordsCount.Text = dgvLocalLicenses.Rows.Count.ToString();

            if (dgvLocalLicenses.Rows.Count > 0)
            {
                dgvLocalLicenses.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicenses.Columns[0].Width = 110;

                dgvLocalLicenses.Columns[1].HeaderText = "App.ID";
                dgvLocalLicenses.Columns[1].Width = 110;

                dgvLocalLicenses.Columns[2].HeaderText = "Class Name";
                dgvLocalLicenses.Columns[2].Width = 250;

                dgvLocalLicenses.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicenses.Columns[3].Width = 150;

                dgvLocalLicenses.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicenses.Columns[4].Width = 150;

                dgvLocalLicenses.Columns[5].HeaderText = "Is Active";
                dgvLocalLicenses.Columns[5].Width = 80;


            }
        }
        private void _LoadInternationalLicenseInfo()
        {
            _dtDriverInternationalLicenseHistory = clsInternationalLicense.GetDriverInternationalLicenses(_DriverID);

            dgvInternationalLicenses.DataSource = _dtDriverInternationalLicenseHistory;

            lblInternationalRecordsCount.Text = dgvInternationalLicenses.Rows.Count.ToString();
            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Lic.ID";
                dgvInternationalLicenses.Columns[0].Width = 110;

                dgvInternationalLicenses.Columns[1].HeaderText = "App.ID";
                dgvInternationalLicenses.Columns[1].Width = 110;

                dgvInternationalLicenses.Columns[2].HeaderText = "Class Name";
                dgvInternationalLicenses.Columns[2].Width = 250;

                dgvInternationalLicenses.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[3].Width = 150;

                dgvInternationalLicenses.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[4].Width = 150;

                dgvInternationalLicenses.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[5].Width = 80;
            }
        }
        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = clsDriver.FindDriverByID(_DriverID);

            if (_Driver == null)
            {
                MessageBox.Show("There is no Driver with ID = " + _DriverID, "Driver Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
        }
        public void LoadInfoByPerson(int PersonID)
        {
            _Driver = clsDriver.FindDriverByPersonID(PersonID);
            if (_Driver == null)
            {
                MessageBox.Show("There is no Driver linked with this Person with ID = " + PersonID, "Driver Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _DriverID = _Driver.DriverID;

            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
        }
        public void Clear()
        {
            _dtDriverInternationalLicenseHistory.Clear();
            _dtDriverLocalLicenseHistory.Clear();
        }

        private void tsmshowLocalLicenseInfo_Click(object sender, System.EventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((int)dgvLocalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void tsmshowInternationalLicenseInfo_Click(object sender, System.EventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo((int)dgvInternationalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
