using System;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmPlayVideo : Form
    {
        public frmPlayVideo()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            //wmpProject.Play
            string VideoPath = @"C:\Users\Windows10\Desktop\20230922-224340.mp4";

            wmpProject.URL = VideoPath;
            wmpProject.Ctlcontrols.play();

        }
    }
}
