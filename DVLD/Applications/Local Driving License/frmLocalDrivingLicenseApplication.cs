using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmLDL : Form
    {
        public frmLDL()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            TCAddUser.SelectedIndex = 1;
        }

        private void btnAddEditPersonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }






    }
}
