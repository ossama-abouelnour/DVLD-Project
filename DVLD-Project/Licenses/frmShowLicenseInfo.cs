using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Licenses
{
    public partial class frmShowLicenseInfo : Form
    {
        private int _DrivingLicenseID = -1;
        public frmShowLicenseInfo(int DrivingLicenseID)
        {
            InitializeComponent();
            _DrivingLicenseID = DrivingLicenseID;
        }

        private void frmShowLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicense1.LoadInfo(_DrivingLicenseID);    
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
