using DVLD.Applications.Local_Driving_License;
using DVLD_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmListLocalDrivingLicenseApp : Form
    {
        private static DataTable _dtAllLocalDrivingLicenseApplications;


        public frmListLocalDrivingLicenseApp()
        {
            InitializeComponent();
        }


        private void frmListLocalDrivingLicenseApp_Load(object sender, EventArgs e)
        {
            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvApplications.DataSource = _dtAllLocalDrivingLicenseApplications;
            lblRecordsCount.Text = dgvApplications.Rows.Count.ToString();
            if (dgvApplications.Rows.Count > 0)
            {
                dgvApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvApplications.Columns[0].Width = 100;

                dgvApplications.Columns[1].HeaderText = "Driving Class";
                dgvApplications.Columns[1].Width = 220;

                dgvApplications.Columns[2].HeaderText = "National No.";
                dgvApplications.Columns[2].Width = 100;

                dgvApplications.Columns[3].HeaderText = "Full Name";
                dgvApplications.Columns[3].Width = 300;

                dgvApplications.Columns[4].HeaderText = "Application Date";
                dgvApplications.Columns[4].Width = 140;

                dgvApplications.Columns[5].HeaderText = "Passed Tests";
                dgvApplications.Columns[5].Width = 100;

                dgvApplications.Columns[6].HeaderText = "Status";
                dgvApplications.Columns[6].Width = 100;
            }
            cbFilterBy.SelectedIndex = 0;
        }


        private void btnAddNewLDLApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();
            frmListLocalDrivingLicenseApp_Load(null, null);
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Status":
                    FilterColumn = "Status";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvApplications.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvApplications.Rows.Count.ToString();
        }

        private void tsmShowDetials_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo((int)dgvApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListLocalDrivingLicenseApp_Load(null, null);
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication((int)dgvApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListLocalDrivingLicenseApp_Load(null, null);

        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Application?", "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByID(LocalDrivingLicenseApplicationID);
            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication())
                {

                    MessageBox.Show("Application With ID " + LocalDrivingLicenseApplicationID + " Was deleted Successfully!", "Deleted", MessageBoxButtons.OK);
                    frmListLocalDrivingLicenseApp_Load(null, null);
                }
                else
                    MessageBox.Show("Application With ID " + LocalDrivingLicenseApplicationID + " Was Not deleted Successfully!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFilterBy.Text == "L.D.L.AppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tsmCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure to Cancel this Application?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            int LocalDrivingLicenseApp = (int)dgvApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication ldlapp = clsLocalDrivingLicenseApplication.FindByID(LocalDrivingLicenseApp);
            if (ldlapp != null)
            {
                if (ldlapp.Cancel())
                {

                    MessageBox.Show("Application With ID " + LocalDrivingLicenseApp + " Was Canceled Successfully!", "Deleted", MessageBoxButtons.OK);
                    frmListLocalDrivingLicenseApp_Load(null, null);
                }
                else
                    MessageBox.Show("Application With ID " + LocalDrivingLicenseApp + " Was Not Canceled Successfully!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvApplications_MouseClick(object sender, MouseEventArgs e)
        {
            if ((string)dgvApplications.CurrentRow.Cells[6].Value == "Cancelled")
            {
                tsmCancel.Enabled = false;
                tsmEdit.Enabled = false;
                tsmScheduleTests.Enabled = false;
                tsmIssueDrivingLicense.Enabled = false;
                tsmShowLicense.Enabled = false;
            }
            else
            {
                tsmCancel.Enabled = true;
                tsmEdit.Enabled = true;
                tsmScheduleTests.Enabled = true;
            }
        }

        private void cmApplication_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                    clsLocalDrivingLicenseApplication.FindByID(LocalDrivingLicenseApplicationID);


            int TotalPassedTests = (int)dgvApplications.CurrentRow.Cells[5].Value;

            bool LicenseExists = LocalDrivingLicenseApplication.IsLicenseIssued();

            //Enabled only if person passed all tests and Does not have a license for the same license class. 
            tsmIssueDrivingLicense.Enabled = (TotalPassedTests == 3) && !LicenseExists;

            tsmShowLicense.Enabled = (TotalPassedTests == 3) && LicenseExists;

            tsmEdit.Enabled = !LicenseExists && (LocalDrivingLicenseApplication.Status == clsApplication.enApplicationStatus.New);
            //ScheduleTestsMenue.Enabled = !LicenseExists;

            //Enable/Disable Cancel Menue Item
            //We only canel the applications with status=new.
            tsmCancel.Enabled = (LocalDrivingLicenseApplication.Status == clsApplication.enApplicationStatus.New);

            //Enable/Disable Delete Menue Item
            //We only allow delete incase the application status is new not complete or Cancelled.
            tsmDelete.Enabled =
                (LocalDrivingLicenseApplication.Status == clsApplication.enApplicationStatus.New);



            //Enable Disable Schedule menue and it's sub menue
            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest); ;
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);

            tsmScheduleTests.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (LocalDrivingLicenseApplication.Status == clsApplication.enApplicationStatus.New);

            if (tsmScheduleTests.Enabled)
            {
                //To Allow Schdule vision test, Person must not passed the same test before.
                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;

                //To Allow Schdule written test, Person must pass the vision test and must not passed the same test before.
                scheduleWritternTestToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;

                //To Allow Schdule steet test, Person must pass the vision * written tests, and must not passed the same test before.
                scheduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;

            }



        }

        private void _ScheduleTest(clsTestType.enTestType TestType)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvApplications.CurrentRow.Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();
            frmListLocalDrivingLicenseApp_Load(null, null);
        }
        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.VisionTest);
        }

        private void scheduleWritternTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.WrittenTest);
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.StreetTest);
        }

        private void tsmIssueDrivingLicense_Click(object sender, EventArgs e)
        {
            frmIssueDLFirstTime frm = new frmIssueDLFirstTime((int)dgvApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListLocalDrivingLicenseApp_Load(null, null);
        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByID((int)dgvApplications.CurrentRow.Cells[0].Value);
            int LicenseID = clsLicense.GetActiveLicenseIdByPersonID(localDrivingLicenseApplication.PersonID, localDrivingLicenseApplication.LicenseClassID);
            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void tsmshowPersonLicenseHistory_Click(object sender, EventArgs e)
        {

            clsPerson person = clsPerson.Find(dgvApplications.CurrentRow.Cells[2].Value.ToString());
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(person.PersonID);
            frm.ShowDialog();
        }
    }
}
