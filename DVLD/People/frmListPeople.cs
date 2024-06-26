﻿using DVLD.People;
using DVLD_Business;
using System.Data;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmListPeople : Form
    {

        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();

        //only select the columns that you want to show in grid 
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName",
        "ThirdName", "LastName", "DateOfBirth"
            , "GenderCaption", "CountryName", "Phone", "Email");
        private void _RefreshPeoplList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName",
        "ThirdName", "LastName", "DateOfBirth"
            , "GenderCaption", "CountryName", "Phone", "Email");

            dgvPeople.DataSource = _dtPeople;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }
        public frmListPeople()
        {
            InitializeComponent();
        }
        private void frmListPeople_Load(object sender, System.EventArgs e)
        {

            dgvPeople.DataSource = _dtPeople;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();

            if (dgvPeople.Rows.Count > 0)
            {
                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 80;

                dgvPeople.Columns[1].HeaderText = "National No.";
                dgvPeople.Columns[1].Width = 85;

                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 120;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 140;

                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 120;

                dgvPeople.Columns[6].HeaderText = "Date of Birth";
                dgvPeople.Columns[6].Width = 140;

                dgvPeople.Columns[7].HeaderText = "Gender";
                dgvPeople.Columns[7].Width = 60;

                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 80;

                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 120;

                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 170;
            }
        }
        private void button1_Click(object sender, System.EventArgs e)
        {
            frmAddUpdatePerson AEPerson = new frmAddUpdatePerson();

            AEPerson.DataBack += PlayVideo;
            //Refresh People List after adding new person.
            AEPerson.ShowDialog();
            _RefreshPeoplList();
        }
        private void PlayVideo(object sender, int PersonID)
        {

            if (PersonID != -1)
            {

                frmPlayVideo frm = new frmPlayVideo();
                frm.ShowDialog();
            }
        }
        private void btnCloseManagePeople_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        private void tsmShowDetials_Click(object sender, System.EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dgvPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeoplList();
        }
        private void addNewPersonToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            frmAddUpdatePerson AEPerson = new frmAddUpdatePerson();
            //to subscripe to delegate, we have to put subscription before showDialog func.
            AEPerson.DataBack += PlayVideo;
            AEPerson.ShowDialog();
            _RefreshPeoplList();
        }
        private void editToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

            frmAddUpdatePerson AEPerson = new frmAddUpdatePerson((int)dgvPeople.CurrentRow.Cells[0].Value);

            AEPerson.ShowDialog();

            _RefreshPeoplList();
        }
        private void deleteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            if (MessageBox.Show("Do you want to delete this Person?", "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (clsPerson.DeletePerson(PersonID))
                {
                    MessageBox.Show("Person With ID " + PersonID + " Was deleted Successfully!", "Deleted", MessageBoxButtons.OK);
                    _RefreshPeoplList();
                }
                else
                    MessageBox.Show("Person With ID " + PersonID + " Was Not deleted Successfully!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }


        }
        private void txtFilterValue_TextChanged(object sender, System.EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Gender":
                    FilterColumn = "GenderCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset filter in case nothing selected or filter value contains none.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "PersonID")
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, System.EventArgs e)
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
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
