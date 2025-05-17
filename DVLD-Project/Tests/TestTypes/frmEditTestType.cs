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
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            _TestType = clsTestType.Find(_TestTypeID);

            if (_TestType == null) 
            {
                MessageBox.Show("Test ID Does Not Exist", "Not Found", MessageBoxButtons.OK);
                this.Close();
            }

            else
            {
                lblTestID.Text = ((int)_TestType.TestTypeID).ToString();
                txtTitle.Text = _TestType.TestTypeTitle;
                txtDescription.Text = _TestType.TestTypeDescription;
                txtFees.Text = _TestType.Fees.ToString();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!", "Please hover over the red icon(s)", MessageBoxButtons.OK);
                return;
            }
            _TestType.TestTypeTitle = txtTitle.Text.Trim();
            _TestType.TestTypeDescription = txtDescription.Text.Trim();
            _TestType.Fees = Convert.ToSingle(txtFees.Text.Trim());

            if (_TestType.Save())
            {
                MessageBox.Show("Details Saved", "Successfull", MessageBoxButtons.OK);

            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if(txtTitle.Text == string.Empty)
            {
                errorProvider1.SetError(txtTitle , "Required Field");
            }

            else
                errorProvider1.SetError(txtTitle, null);

        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (txtDescription.Text == string.Empty)
            {
                errorProvider1.SetError(txtDescription, "Required Field");
            }

            else 
                errorProvider1.SetError(txtDescription, null);

        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (txtFees.Text == string.Empty)
            {
                errorProvider1.SetError(txtFees, "Required Field");
            }

            else
                errorProvider1.SetError(txtFees, null);
        }
    }
}
