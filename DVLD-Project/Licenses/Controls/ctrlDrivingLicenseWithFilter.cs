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

namespace DVLD_Project.Licenses.Controls
{
    public partial class ctrlDrivingLicenseWithFilter : UserControl
    {
        public event Action<int> OnLicenseSelected;
        protected virtual void SelectedLicense(int LicenseID)
        {
            Action<int> hander = OnLicenseSelected;

            if (hander != null)
            {
                hander(LicenseID);
            }
        }
        public ctrlDrivingLicenseWithFilter()
        {
            InitializeComponent();
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            { 
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

        private int _LicenseID = -1;
        public int LicenseID
        {
            get { return ctrlDrivingLicense1.LicenseID; }
        }

        public clsLicense SelectedLicenseInfo
        {
            get { return ctrlDrivingLicense1.SelectedLicenseInfo; }
        }

        public void LoadLicenseInfo(int LicenseID)
        {
            txtLicenseID.Text = LicenseID.ToString();
            ctrlDrivingLicense1.LoadInfo(LicenseID);
            _LicenseID = ctrlDrivingLicense1.LicenseID;

            if(OnLicenseSelected != null && FilterEnabled)
            {
                OnLicenseSelected(LicenseID);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid", "Hover over the red icon");
                txtLicenseID.Focus();
                return;
            }

            _LicenseID = int.Parse(txtLicenseID.Text.Trim());
            LoadLicenseInfo(LicenseID);
        }

        public void txtLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                errorProvider1.SetError(txtLicenseID, "This field is required");
            }

            else
            {
                errorProvider1.SetError(txtLicenseID, null);
            }
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if(e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
        }
    }
}
