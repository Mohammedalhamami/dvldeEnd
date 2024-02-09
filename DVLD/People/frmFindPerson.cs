using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmFindPerson : Form
    {
        //Declare delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);
        //declare event of type delegate.
        public event DataBackEventHandler DataBack;
        public frmFindPerson()
        {
            InitializeComponent();

        }

        private void btnPersonDetailsClose_Click(object sender, EventArgs e)
        {
            //Trigger the event to send databack to the caller form.
            DataBack?.Invoke(this, ctlPersonCardWithFilter1.PersonID);
            this.Close();
        }
    }
}
