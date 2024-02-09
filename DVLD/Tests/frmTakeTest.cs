using DVLD.Global_Classes;
using DVLD_Business;
using System;

using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class frmTakeTest : Form
    {
        private int _TestAppointmentID = -1;
        private clsTestType.enTestType _TestTypeID;

        private int _TestID = -1;
        private clsTest _Test;
        public frmTakeTest(int TestAppointmentID, clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to save?, after that you can not change valus.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            _Test.TestAppointmentID = _TestAppointmentID;
            _Test.Notes = tbTestNote.Text.Trim();
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _Test.TestResult = rbPass.Checked;


            if (_Test.Save())
            {

                MessageBox.Show("Test Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;

            }
            else
                MessageBox.Show("Error: Test is not Saved Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctlScheduledTest1.TestTypeID = _TestTypeID;
            ctlScheduledTest1.LoadInfo(_TestAppointmentID);

            if (ctlScheduledTest1.TestAppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

            _TestID = ctlScheduledTest1.TestID;

            if (_TestID != -1)
            {
                _Test = clsTest.Find(_TestID);

                if (_Test.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;
                tbTestNote.Text = _Test.Notes;

                lblPostSave.Visible = true;
                rbPass.Enabled = false;
                rbFail.Enabled = false;
            }
            else
            {
                _Test = new clsTest();
            }


        }
    }
}
