using DVLD_Business;
using DVLD_Project.Licenses;
using DVLD_Project.People.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.International_License
{
    public partial class frmListInternationalLicenseApplications : Form
    {
        private DataTable _dt;
        
        public frmListInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNewApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
            frmListInternationalLicenseApplications_Load(null, null);
        }

        private void frmListInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _dt = clsInternationalLicense.GetAllInternationalLicenses();
            cbFilterBy.SelectedIndex = 0;

            dgvILlDrivingLicenseApplications.DataSource = _dt;
            lblRecordsCount.Text =dgvILlDrivingLicenseApplications.Rows.Count.ToString();

            if(dgvILlDrivingLicenseApplications.Rows.Count > 0)
            {
                dgvILlDrivingLicenseApplications.Columns[0].HeaderText = "Int.License ID";
                dgvILlDrivingLicenseApplications.Columns[0].Width = 160;

                dgvILlDrivingLicenseApplications.Columns[1].HeaderText = "Application ID";
                dgvILlDrivingLicenseApplications.Columns[1].Width = 150;

                dgvILlDrivingLicenseApplications.Columns[2].HeaderText = "Driver ID";
                dgvILlDrivingLicenseApplications.Columns[2].Width = 130;

                dgvILlDrivingLicenseApplications.Columns[3].HeaderText = "L.License ID";
                dgvILlDrivingLicenseApplications.Columns[3].Width = 130;

                dgvILlDrivingLicenseApplications.Columns[4].HeaderText = "Issue Date";
                dgvILlDrivingLicenseApplications.Columns[4].Width = 180;

                dgvILlDrivingLicenseApplications.Columns[5].HeaderText = "Expiration Date";
                dgvILlDrivingLicenseApplications.Columns[5].Width = 180;

                dgvILlDrivingLicenseApplications.Columns[6].HeaderText = "Active";
                dgvILlDrivingLicenseApplications.Columns[6].Width = 120;
            }
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvILlDrivingLicenseApplications.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByPersonID(DriverID).PersonID;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvILlDrivingLicenseApplications.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByPersonID(DriverID).PersonID;
            frmShowLicenseHistory frm = new frmShowLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvILlDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmShowLicenseInfo frm = new frmShowLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();

        }
    }
}
