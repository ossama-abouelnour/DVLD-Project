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

namespace DVLD_Project.Tests
{
    public partial class frmListTestAppointments : Form
    {
        private DataTable _dtLicenseTestAppointments;
        private int _LocalDrivingLicenseApplicationID;
        private clsTestType.enTestType _TestType = clsTestType.enTestType.EyeTest;

        public frmListTestAppointments(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestType = TestType;
        }

        private void _UpdateTitle()
        {
            switch(_TestType)
            {
                case clsTestType.enTestType.TheoryTest:
                    lblTitle.Text = "Theory Test Appointments";
                    break;
                case clsTestType.enTestType.EyeTest:
                    lblTitle.Text = "Eye Test Appointments";
                    break;
                case clsTestType.enTestType.DrivingTest:
                    lblTitle.Text = "Driving Test Appointments";
                    break;
            }
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _UpdateTitle();

            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);
            _dtLicenseTestAppointments = clsTestAppointment.GetApplicationTestAppointmentPerTestType(_LocalDrivingLicenseApplicationID, _TestType);

            dgvAppointments.DataSource = _dtLicenseTestAppointments;
            lblRecordsCount.Text = dgvAppointments.Rows.Count.ToString();

            if (dgvAppointments.Rows.Count > 0)
            {
                dgvAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvAppointments.Columns[0].Width = 150;

                dgvAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvAppointments.Columns[1].Width = 200;

                dgvAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvAppointments.Columns[2].Width = 150;

                dgvAppointments.Columns[3].HeaderText = "Locked?";
                dgvAppointments.Columns[3].Width = 100;
            }
        }

        private void btnNewAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("This person already has a scheduled appointment", "Existing Appointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //clsTest LastTest = LocalDrivingLicenseApplication.GetLastTestPerTestType(_TestType);
            //if (LastTest == null)
            {
                frmScheduleTest frm1 = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType);
                frm1.ShowDialog();
                frmListTestAppointments_Load(null, null);
                return;
            }

            //if (LastTest.TestResult == true)
            {
                //MessageBox.Show("This person already passed this test before", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }

            //frmScheduleTest frm2 = new frmScheduleTest(LastTest.TestAppointmentInfo.LocalDrivingLicenseApplicationID, _TestType);
            //frm2.ShowDialog();
            //frmListTestAppointments_Load(null, null);

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvAppointments.CurrentRow.Cells[0].Value;

            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType, TestAppointmentID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvAppointments.CurrentRow.Cells[0].Value;

            //frmTakeTest frm = new frmTakeTest(TestAppointmentID, _TestType);
            //frm.ShowDialog();
            //frmListTestAppointments_Load(null, null);
        }
    }
}
