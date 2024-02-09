using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmShowPersonInfo : Form
    {
        public frmShowPersonInfo(int PersonID)
        {
            InitializeComponent();
            ctlPersonCard1.LoadPersonInfo(PersonID);
        }
        public frmShowPersonInfo(string NationalNo)
        {
            InitializeComponent();
            ctlPersonCard1.LoadPersonInfo(NationalNo);
        }

        private void btnPersonDetailsClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
