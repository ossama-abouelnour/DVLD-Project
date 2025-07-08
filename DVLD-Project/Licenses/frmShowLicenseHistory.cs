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
    public partial class frmShowLicenseHistory : Form
    {
        private int _PersonID = -1;

        public frmShowLicenseHistory()
        {
            InitializeComponent();
        }

        public frmShowLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }
        private void frmShowLicenseHistory_Load(object sender, EventArgs e)
        {
            if(_PersonID != -1)
            {
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                ctrlPersonCardWithFilter1.FilterEnabled = false;
                ctrlDriverLicensesHistory1.LoadInfoByPersonID(_PersonID);
            }

            else
            {
                ctrlPersonCardWithFilter1.FilterEnabled = true;
                ctrlPersonCardWithFilter1.Focus();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;

            if(_PersonID == -1)
            {
                ctrlDriverLicensesHistory1.Clear();
            }
            else
                ctrlDriverLicensesHistory1.LoadInfoByPersonID(_PersonID);
        }
    }
}
