using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmShowPersonLicenseHistory : Form
    {

        private int _PersonID = -1;
        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();
        }
        public frmShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }



        private void btnAddEditPersonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                ctlDriverLicenses1.LoadInfoByPerson(_PersonID);
                ctlPersonCardWithFilter1.FilterEnabled = false;

            }
            else
            {
                ctlPersonCardWithFilter1.FilterEnabled = true;
                ctlPersonCardWithFilter1.FilterFocus();

            }




        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;

            if (_PersonID == -1)
            {
                ctlDriverLicenses1.Clear();
            }
            else
                ctlDriverLicenses1.LoadInfoByPerson(_PersonID);
        }
    }
}
