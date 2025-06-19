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
    public partial class ctrlDriverLicensesHistory : UserControl
    {
        private int _DriverID;
        private clsDriver _Driver;
        private DataTable _dtLocalLicenseHistory;
        private DataTable _dtInternationalLicenseHistory;

        public ctrlDriverLicensesHistory()
        {
            InitializeComponent();
        }

        private void _LoadInternationalLicenseInfo()
        {

            //_dtInternationalLicenseHistory = clsDriver.GetInternationalLicenses(_DriverID);


            dgvInternationalLicensesHistory.DataSource = _dtInternationalLicenseHistory;
            lblInternationalLicensesRecords.Text = dgvInternationalLicensesHistory.Rows.Count.ToString();

            if (dgvInternationalLicensesHistory.Rows.Count > 0)
            {
                dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicensesHistory.Columns[0].Width = 160;

                dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicensesHistory.Columns[1].Width = 130;

                dgvInternationalLicensesHistory.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicensesHistory.Columns[2].Width = 130;

                dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicensesHistory.Columns[3].Width = 180;

                dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicensesHistory.Columns[4].Width = 180;

                dgvInternationalLicensesHistory.Columns[5].HeaderText = "Active";
                dgvInternationalLicensesHistory.Columns[5].Width = 120;

            }
        }
        private void _LoadLocalLicenseInfo()
        {
            _dtLocalLicenseHistory = clsDriver.GetLicenses(_DriverID);

            dgvLocalLicensesHistory.DataSource = _dtLocalLicenseHistory;

            lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.RowCount.ToString();

            if(dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic. ID";
                dgvLocalLicensesHistory.Columns[0].Width = 110;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 110;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 270;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 170;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 170;

                dgvLocalLicensesHistory.Columns[5].HeaderText = "Active";
                dgvLocalLicensesHistory.Columns[5].Width = 110;
            }
        }
        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;

            _Driver = clsDriver.FindByDriverID(DriverID);

            if (_Driver == null)
            {
                MessageBox.Show("No Driver Found With ID " + _DriverID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();

        }
        public void LoadInfoByPersonID(int PersonID)
        {
            _Driver = clsDriver.FindByPersonID(PersonID);


            if (_Driver == null)
            {
                MessageBox.Show("No Person Found With ID " + PersonID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _DriverID = _Driver.DriverID;

            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();

        }
        public void Clear() 
        {
            _dtLocalLicenseHistory.Clear();
            _dtInternationalLicenseHistory.Clear();
        }
    }
}
