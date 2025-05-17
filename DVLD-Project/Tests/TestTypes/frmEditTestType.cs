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

namespace DVLD_Project.Tests.TestTypes
{
    public partial class frmEditTestType : Form
    {
        private clsTestType.enTestType _TestTypeID;
        private clsTestType _TestType;

        public frmEditTestType(clsTestType.enTestType TestTypeID)
        {
            _TestTypeID = TestTypeID;
            InitializeComponent();
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            _TestType = clsTestType.Find(_TestTypeID);

            if (_TestType == null) 
            {
                MessageBox.Show("Test ID Does Not Exist", "Not Found", MessageBoxButtons.OK);
                return;
            }

            else
            {
                lblTestID.Text = _TestType.TestTypeID.ToString();
                txtTitle.Text = _TestType.TestTypeTitle;
                txtDescription.Text = _TestType.TestTypeDescription;
                txtFees.Text = _TestType.Fees.ToString();
            }

        }
    }
}
