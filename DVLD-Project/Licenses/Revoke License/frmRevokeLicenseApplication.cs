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

namespace DVLD_Project.Licenses.Revoke_License
{
    public partial class frmRevokeLicenseApplication : Form
    {
        private int _DetainID = -1;
        private int _SelectedLicenseID = -1;
        public frmRevokeLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmRevokeLicenseApplication_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void ctrlDrivingLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;
            lblLicenseID.Text = _SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1)
                return;
            if(ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.IsRevoked)
            {
                MessageBox.Show("License is already revoked", "Already Revoked", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtFineFees.Focus();
            btnRevoke.Enabled = true;
        }

        private void btnRevoke_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to revoke this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            _DetainID = ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.Revoke(Convert.ToSingle(txtFineFees.Text), clsGlobal.CurrentUser.UserID);

            if(_DetainID == -1)
            {
                MessageBox.Show("Failed to revoke license. Contact System Admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblDetainID.Text = _DetainID.ToString();
            MessageBox.Show("License Revoked Successfully. Revoke ID = "+ _DetainID.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnRevoke.Enabled = false;
            ctrlDrivingLicenseWithFilter1.FilterEnabled = false;
            txtFineFees.Enabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseHistory frm = new frmShowLicenseHistory(ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }
    }
}
