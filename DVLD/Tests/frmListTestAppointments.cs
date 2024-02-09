using DVLD.Properties;
using DVLD.Tests;
using DVLD.Tests.Test_Types;
using DVLD_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmListTestAppointments : Form
    {

        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestType.enTestType _TestTypeID;

        DataTable _dtTestType;
        public frmListTestAppointments(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
        }

        private void _LoadTestTypeImageAndTitle()
        {
            if (_TestTypeID == clsTestType.enTestType.VisionTest)
            {
                pbTestType.Image = Resources.Vision_512;
                lblTestTypeTitle.Text = "Vision Test Appointment";
            }
            else if (_TestTypeID == clsTestType.enTestType.WrittenTest)
            {
                pbTestType.Image = Resources.Written_Test_512;
                lblTestTypeTitle.Text = "Written Test Appointment";
            }
            else
            {
                pbTestType.Image = Resources.TestType_512;
                lblTestTypeTitle.Text = "Street Test Appointment";
            }
        }
        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();


            ctlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);

            DataTable _dtTestAppointments = clsTestAppointments.GetApplicationTestAppointmentPerTestType(_LocalDrivingLicenseApplicationID, (int)_TestTypeID);
            dgvTestAppointments.DataSource = _dtTestAppointments;
            lblRecords.Text = dgvTestAppointments.Rows.Count.ToString();

            if (dgvTestAppointments.Rows.Count > 0)
            {
                dgvTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvTestAppointments.Columns[0].Width = 150;

                dgvTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvTestAppointments.Columns[1].Width = 200;

                dgvTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvTestAppointments.Columns[2].Width = 150;

                dgvTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvTestAppointments.Columns[3].Width = 80;
                tsmEditTestAppointment.Enabled = !(bool)dgvTestAppointments.CurrentRow.Cells[3].Value;
                tsmtakeTest.Enabled = tsmEditTestAppointment.Enabled;
            }



        }





        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmEditTestAppointment_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvTestAppointments.CurrentRow.Cells[0].Value;
            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID, TestAppointmentID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }

        private void tsmtakeTest_Click(object sender, EventArgs e)
        {
            frmTakeTest frm = new frmTakeTest((int)dgvTestAppointments.CurrentRow.Cells[0].Value, _TestTypeID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByID(_LocalDrivingLicenseApplicationID);
            if (LocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestTypeID))
            {
                MessageBox.Show("Person Already has Active Appointment for this test", "Active Appointment", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            clsTest LastTest = LocalDrivingLicenseApplication.GetLastTestPerTestType(_TestTypeID);

            if (LastTest == null)
            {
                frmScheduleTest fm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID);
                fm.ShowDialog();
                frmListTestAppointments_Load(null, null);
                return;
            }

            //if person already passsed the test, they can retake it
            if (LastTest.TestResult == true)
            {
                MessageBox.Show("This Applicant is already passed this test.", "Passed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
            return;
        }
    }
}
