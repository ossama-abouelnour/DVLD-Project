using DVLD_Business;
using DVLD_Project.Global_Classes;
using DVLD_Project.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.International_License
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        private int _InternationalLicenseID = -1;
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void ctrlDrivingLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            int selectedLicense = obj;

            lblLocalLicenseID.Text = selectedLicense.ToString();
            llShowLicenseHistory.Enabled = (selectedLicense != -1);

            if(selectedLicense == -1)
            {
                return;
            }

            if(ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("Selected License Should Be Class 3", "Wrong License Class", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ActiveInternationalLicense = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.DriverID);

            if(ActiveInternationalLicense != -1)
            {
                MessageBox.Show("This driver already have an active international license with number " + ActiveInternationalLicense, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = true;
                _InternationalLicenseID = ActiveInternationalLicense;
                btnIssue.Enabled = false;
                return;
            }
            btnIssue.Enabled = true;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue an international license", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            { 
                return; 
            }

            clsInternationalLicense InternationalLicense = new clsInternationalLicense();


            InternationalLicense.ApplicantPersonID = ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicence).Fee;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            InternationalLicense.DriverID = ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrlDrivingLicenseWithFilter1.SelectedLicenseInfo.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if(!InternationalLicense.Save())
            {
                MessageBox.Show("Failed to issue a new international license, contact admin ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblILLicenseID.Text = _InternationalLicenseID.ToString();
            MessageBox.Show("License issue successfully. License ID is: " + _InternationalLicenseID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnIssue.Enabled = false;
            ctrlDrivingLicenseWithFilter1.FilterEnabled = false;
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
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_InternationalLicenseID);
            frm.ShowDialog();

        }
    }
}
