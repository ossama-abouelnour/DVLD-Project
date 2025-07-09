using DVLD_Business;
using DVLD_Project.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.ReleaseRevokedLicense
{
    public partial class frmReleaseRevokedLicenseApplication : Form
    {
        private int _SelectedLicenseID = -1;
        public frmReleaseRevokedLicenseApplication()
        {
            InitializeComponent();
        }

        public frmReleaseRevokedLicenseApplication(int LicenseID)
        {
            _SelectedLicenseID = LicenseID;
            InitializeComponent();
            ctrlDrivingLicenseWithFilter1.LoadLicenseInfo(_SelectedLicenseID);
            ctrlDrivingLicenseWithFilter1.FilterEnabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlDrivingLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            lblLicenseID.Text = _SelectedLicenseID.ToString();

            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1)
                return;

            if(!ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.IsRevoked)
            {
                MessageBox.Show("License is is not revoked", "Not Revoked", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseRevokedLicence).ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblDetainID.Text = ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.RevokedInfo.RevokeID.ToString();
            lblLicenseID.Text = ctrlDrivingLicenseWithFilter1 .SelectedLicenseInfo.LicenseID.ToString();
            lblDetainDate.Text = ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.RevokedInfo.RevokeDate.ToShortDateString();
            lblFineFees.Text = ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.RevokedInfo.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToString(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text));
            btnRelease.Enabled = true;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            int ApplicationID = -1;
            bool isReleased = ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.ReleaseRevokedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID);

            lblApplicationID.Text = ApplicationID.ToString();

            if (isReleased)
            {
                MessageBox.Show("Failed to release revoked license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("License released successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Error);
            btnRelease.Enabled = false;
            ctrlDrivingLicenseWithFilter1.FilterEnabled = false;
            llShowLicenseHistory.Enabled = true;
        }
    }
}
