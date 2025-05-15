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

namespace DVLD_Project.Applications.Application_Types
{
    public partial class frmEditApplicationType : Form
    {
        private int _ApplicationTypeID = -1;
        private clsApplicationType _ApplicationType;
        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {

            _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);

            if (_ApplicationType != null)
            {
                MessageBox.Show("Details: ", _ApplicationType.Title.ToString() + " " + _ApplicationType.Fee.ToString() , MessageBoxButtons.OK);

                lblID.Text = _ApplicationTypeID.ToString();
                txtTitle.Text = _ApplicationType.Title;
                txtFees.Text = _ApplicationType.Fee.ToString();
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
                MessageBox.Show("Some fields are not valid", "Validation Error", MessageBoxButtons.OK);
                return;
            }

            _ApplicationType.Title = txtTitle.Text.Trim(); 
            _ApplicationType.Fee = Convert.ToSingle(txtFees.Text.Trim());

            if(_ApplicationType.Save())
            {
                MessageBox.Show("Application Info Updated Successfully", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else
                MessageBox.Show("Save Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
