using DVLD_Business;
using DVLD_Project.Licenses;
using DVLD_Project.Licenses.Revoke_License;
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

namespace DVLD_Project.Applications.ReleaseRevokedLicense
{
    public partial class frmListRevokedLicenses : Form
    {
        private DataTable _dtRevokedLicenses;
        public frmListRevokedLicenses()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListRevokedLicenses_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;

            _dtRevokedLicenses = clsRevokedLicense.GetAllDetainedLicenses();

            dgvRevokedLicenses.DataSource = _dtRevokedLicenses;
            lblRecordsCount.Text = dgvRevokedLicenses.Rows.Count.ToString();

            if(dgvRevokedLicenses.Rows.Count > 0)
            {
                dgvRevokedLicenses.Columns[0].HeaderText = "D.ID";
                dgvRevokedLicenses.Columns[0].Width = 90;

                dgvRevokedLicenses.Columns[1].HeaderText = "L.ID";
                dgvRevokedLicenses.Columns[1].Width = 90;

                dgvRevokedLicenses.Columns[2].HeaderText = "D.Date";
                dgvRevokedLicenses.Columns[2].Width = 160;

                dgvRevokedLicenses.Columns[3].HeaderText = "Is Released";
                dgvRevokedLicenses.Columns[3].Width = 110;

                dgvRevokedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvRevokedLicenses.Columns[4].Width = 110;

                dgvRevokedLicenses.Columns[5].HeaderText = "Release Date";
                dgvRevokedLicenses.Columns[5].Width = 160;

                dgvRevokedLicenses.Columns[6].HeaderText = "N.No.";
                dgvRevokedLicenses.Columns[6].Width = 90;

                dgvRevokedLicenses.Columns[7].HeaderText = "Full Name";
                dgvRevokedLicenses.Columns[7].Width = 330;

                dgvRevokedLicenses.Columns[8].HeaderText = "Rlease App.ID";
                dgvRevokedLicenses.Columns[8].Width = 150;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;
                case "Is Released":
                    {
                        FilterColumn = "IsReleased";
                        break;
                    }
                    ;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }


            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtRevokedLicenses.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvRevokedLicenses.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
                //in this case we deal with numbers not string.
                _dtRevokedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtRevokedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _dtRevokedLicenses.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Released")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }

            else

            {

                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsReleased.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    txtFilterValue.Enabled = false;


                }
                else
                    txtFilterValue.Enabled = true;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            releaseLicenseToolStripMenuItem.Enabled = !((bool)dgvRevokedLicenses.CurrentRow.Cells[3].Value);
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsLicense.Find((int)dgvRevokedLicenses.CurrentRow.Cells[1].Value).DriverInfo.PersonID;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
            
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((int)dgvRevokedLicenses.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsLicense.Find((int)dgvRevokedLicenses.CurrentRow.Cells[1].Value).DriverInfo.PersonID;
            frmShowLicenseHistory frm = new frmShowLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void releaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseRevokedLicenseApplication frm = new frmReleaseRevokedLicenseApplication((int)dgvRevokedLicenses.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            frmListRevokedLicenses_Load(null, null);
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseRevokedLicenseApplication frm = new frmReleaseRevokedLicenseApplication();
            frm.ShowDialog();
            frmListRevokedLicenses_Load(null, null);
        }

        private void btnRevoke_Click(object sender, EventArgs e)
        {
            frmRevokeLicenseApplication frm = new frmRevokeLicenseApplication();
            frm.ShowDialog();
            frmListRevokedLicenses_Load(null, null);
        }
    }
}
