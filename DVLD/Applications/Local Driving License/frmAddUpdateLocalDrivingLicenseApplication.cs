using DVLD.Global_Classes;
using DVLD_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {
        public enum enMode { AddNew = 1, Update = 2 }
        private enMode _Mode;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _SelectedPersonID = -1;
        private clsPerson _SelectedPerson;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

        }

        private void _FillLicenseClassesInComboBox()
        {
            DataTable dt = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow row in dt.Rows)
            {
                cbLicenseClasses.Items.Add(row["ClassName"]);
            }
        }
        private void _ResetDefualtValues()
        {
            _FillLicenseClassesInComboBox();
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                tpApplicationInfo.Enabled = false;
                tcAddUpdate.SelectedIndex = 0;
                ctlPersonCardWithFilter1.FilterFocus();
                cbLicenseClasses.SelectedIndex = 2;
                btnSave.Enabled = false;
                // cbLicenseClasses.SelectedIndex = 2;
                lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
            }

        }
        private void _LoadData()
        {
            ctlPersonCardWithFilter1.FilterEnabled = false;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application With ID = " + _LocalDrivingLicenseApplicationID, "No Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ctlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.PersonID);
            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseApplication.Date);
            cbLicenseClasses.SelectedIndex = cbLicenseClasses.FindString(clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName.ToString());
            lblFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedByUser.Text = clsUser.FindByUserID(_LocalDrivingLicenseApplication.CreatedByUserID).UserName.ToString();
        }

        private void frmAddUpdateLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcAddUpdate.SelectedIndex = 1;
                return;
            }
            if (ctlPersonCardWithFilter1.PersonID != -1)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcAddUpdate.SelectedIndex = 1;
                return;
            }
            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctlPersonCardWithFilter1.FilterFocus();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {


            int LicenseClassID = clsLicenseClass.Find(cbLicenseClasses.Text).LicenseClassID;

            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPerson.PersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected person already has this application", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

                cbLicenseClasses.Focus();
                return;
            }
            //to check if user has issued license matching the applicable one.
            if (clsLicense.IsLicenseExistByPersonID(_SelectedPersonID, LicenseClassID))
            {
                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDrivingLicenseApplication.PersonID = ctlPersonCardWithFilter1.PersonID;
            _LocalDrivingLicenseApplication.Date = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID = clsApplication.enApplicationType.NewDrivingLicense;
            _LocalDrivingLicenseApplication.Status = clsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToDecimal(lblFees.Text);
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;

            if (_LocalDrivingLicenseApplication.Save())
            {

                lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }



        private void btnAddEditPersonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddUpdateLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctlPersonCardWithFilter1.FilterFocus();
        }

        private void ctlPersonCardWithFilter1_OnPersonSelected(object sender, clsPerson Person)
        {
            _SelectedPerson = Person;
            ctlPersonCardWithFilter1.LoadPersonInfo(_SelectedPerson.PersonID);
        }
    }
}
