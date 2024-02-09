using DVLD.Global_Classes;
using DVLD.Properties;
using DVLD_Business;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmAddUpdatePerson : Form
    {
        private int _PersonID;
        public enum enMode { AddNew = 0, Update = 1 }

        private enMode _Mode;

        public enum enGender { Male = 0, Female = 1 }

        clsPerson _Person;
        //delcare delegate  
        public delegate void DataBackEventHanlder(object sender, int PersonID);
        //declare an event using the delegate
        public event DataBackEventHanlder DataBack;
        public frmAddUpdatePerson()
        {
            InitializeComponent();
            lblFormTitle.Text = "Add New Person";
            _Mode = enMode.AddNew;

        }
        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            lblFormTitle.Text = "Edit Current Person";
            _Mode = enMode.Update;

            _PersonID = PersonID;
        }

        private void _RestDefualtValues()
        {
            _FillCountriesInComboBox();

            //set defult title
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";

                _Person = new clsPerson();
            }
            else
            {
                lblTitle.Text = "Update Person";
            }
            //set defualt image for person

            if (rbMale.Checked)
            {
                pbPersonImage.Image = Resources.Male_512;
            }
            else
            {
                pbPersonImage.Image = Resources.Female_512;
            }

            //hide or show link lable remove image
            llRemoveImage.Visible = (pbPersonImage.ImageLocation != "");
            //set max date of birth equl to >= 18.
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            //set min date of birth equl to 100.
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            cbCountries.SelectedIndex = cbCountries.FindString("Iraq");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            rbMale.Checked = true;
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";

        }
        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountries.Items.Add(row["CountryName"]);
            }
        }
        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK);
                this.Close();
                return;
            }

            //the following code will not excute if the person was not found
            lblPersonID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            txtEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;
            _Person.Mode = clsPerson.enMode.AddNew;
            if (_Person.Gender == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            txtAddress.Text = _Person.Address;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            txtPhone.Text = _Person.Phone;
            cbCountries.SelectedIndex = cbCountries.FindString(_Person.CountryInfo.CountryName);

            //load person image incase it was set
            if (_Person.ImagePath != "")
            {
                pbPersonImage.ImageLocation = _Person.ImagePath;
            }
            //hide, show the remove link incase there is no image for the person
            llRemoveImage.Visible = (_Person.ImagePath != "");
        }
        private void frmAddUpdatePerson_Load(object sender, System.EventArgs e)
        {
            _RestDefualtValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }
        private bool _HandlePersonImage()
        {
            /*
             * This procedure will handle a person image
             * it will take care of deleting the old image from the folder
             * in case the image was changed.
             * and it will rename new image with GUID and place it in the image folder.
             */
            //Indicated the image was changed.
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                //if yes, this old image in DB will removed.
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        //delete old image from folder
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {

                    }
                }
                //make sure new image was selected.
                if (pbPersonImage.ImageLocation != null)
                {
                    //now we copy image path to save it in image folder after rename it.
                    string SourceImageFile = pbPersonImage.ImageLocation;

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        //load image from the created folder.
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                }
            }
            return true;
        }
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;

            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                Temp.Focus();
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int NationalityCountryID = clsCountry.Find(cbCountries.Text).CountryID;
            if (_Mode == enMode.Update)
                _Person.Mode = clsPerson.enMode.Update;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.NationalityCountryID = NationalityCountryID;

            if (!_HandlePersonImage())
                return;

            if (rbMale.Checked)
                _Person.Gender = (byte)enGender.Male;
            else
                _Person.Gender = (byte)enGender.Female;

            if (pbPersonImage.ImageLocation != null)
                _Person.ImagePath = pbPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";


            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();

                //change form mode to update
                _Mode = enMode.Update;
                lblTitle.Text = "Update Person";

                MessageBox.Show("Data saved successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
                MessageBox.Show("Data is not Saved Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            //no need to validate email incase it is empty!
            if (txtEmail.Text.Trim() == "")
                return;

            if (!clsValidation.ValidateEmail(txtEmail.Text.Trim()))
            {
                e.Cancel = true;
                txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

            if (txtNationalNo.Text.Trim() != _Person.NationalNo && clsPerson.IsPersonExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person");
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == "")
                pbPersonImage.Image = Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == "")
                pbPersonImage.Image = Resources.Female_512;
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPhone.Text.Trim()))
            {
                e.Cancel = true;
                txtPhone.Focus();
                errorProvider1.SetError(txtPhone, "This Field is required!");

            }
            else
            {
                errorProvider1.SetError(txtPhone, null);
            }

            if (!clsValidation.ValidateInteger(txtPhone.Text.Trim()))
            {
                e.Cancel = true;
                txtPhone.Focus();
                errorProvider1.SetError(txtPhone, "Invalid Phone number Format!");
            }
            else
            {
                errorProvider1.SetError(txtPhone, null);
            }

        }

        private void LLabelSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Images files|*.jpg;*.jpeg;*.png;*.svg;*.gif;*.bmp ";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbPersonImage.Load(openFileDialog1.FileName);
                llRemoveImage.Visible = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            llRemoveImage.Visible = false;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DataBack?.Invoke(this, _PersonID);
            this.Close();
        }
    };
}


