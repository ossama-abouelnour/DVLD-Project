using DVLD_Business;
using DVLD_Project.Global_Classes;
using DVLD_Project.Properties;
using System;
using System.Windows.Forms;

namespace DVLD_Project.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        public enum enMode {AddNew =0, Update =1};

        private enMode _Mode = enMode.AddNew;

        public enum enCreationMode { FirstTime = 0, Retake = 1};
        private enCreationMode _CreationMode = enCreationMode.FirstTime;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.EyeTest;

        public clsTestType.enTestType TestTypeID
        {
            get { return _TestTypeID; }

            set 
            { 
            
                _TestTypeID = value; 
                switch(_TestTypeID)
                {
                    case clsTestType.enTestType.EyeTest:
                        gbTestType.Text = "Eye Test";
                        pbTestTypeImage.Image = Resources.eye;
                        break;

                    case clsTestType.enTestType.TheoryTest:
                        gbTestType.Text = "Theory Test";
                        pbTestTypeImage.Image = Resources.test;
                        break;

                    case clsTestType.enTestType.DrivingTest:
                        gbTestType.Text = "Driving Test";
                        pbTestTypeImage.Image = Resources.test_drive;
                        break;
                }
            }
        }

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        private int _LocalDrivingLicenseApplicationID = -1;

        private clsTestAppointment _TestAppointment;

        private int _TestAppointmentID = -1;

        private bool _HandleActiveTestAppointmentConstraint()
        {
            if (_Mode == enMode.AddNew && clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                MessageBox.Show("This Applicant already has an active appointment for this test type" , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }
            return true;
        }

        private bool _HandledAppointmentLockedConstraint()
        {
            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "This person already sat this test, appointment locked";
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;
                return false;
            }

            else
                lblUserMessage.Visible=false;
            return true;
        }

        private bool _HandlePreviousTestConstraint()
        {
            switch(TestTypeID)
            {
                case clsTestType.enTestType.EyeTest:
                    lblUserMessage.Visible = false;
                    return true;
                case clsTestType.enTestType.TheoryTest:
                    if (!_LocalDrivingLicenseApplication.DidPassTestType(clsTestType.enTestType.EyeTest))
                    {
                        lblUserMessage.Text = "Cannot schedule, eye test should be passed first";
                        lblUserMessage.Visible = true ;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled=false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Enabled=false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled=true;
                    }
                    return true ;

                case clsTestType.enTestType.DrivingTest:
                    if(!_LocalDrivingLicenseApplication.DidPassTestType(clsTestType.enTestType.TheoryTest))
                    {
                        lblUserMessage.Text = "Cannot schedule, theory test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }

                    else
                    {
                        lblUserMessage.Enabled = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }
                    return true;
            }
            return true;
        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if (_TestAppointment == null) 
            {
                MessageBox.Show("No local driving license application with ID: " + _LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblFees.Text = _TestAppointment.PaidFees.ToString();

            if(DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dtpTestDate.MinDate = DateTime.Now;
            else
                dtpTestDate.MinDate = _TestAppointment.AppointmentDate;

            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "NA";
            }

            else
            {
                lblRetakeAppFees.Text = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule a Retake";
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
            }    
            return true;
        }

        public void LoadInfo(int LocalDrivingLicenseApplicationID, int AppointmentID = -1)
        {
            if (AppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = AppointmentID;

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No local driving license application with ID: " + _LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            if (_LocalDrivingLicenseApplication.DidAttendTestType(_TestTypeID))
            
                _CreationMode = enCreationMode.Retake;
            else
                _CreationMode = enCreationMode.FirstTime;

            if (_CreationMode == enCreationMode.Retake)
            {
                lblRetakeAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fee.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule a Retake";
                lblRetakeTestAppID.Text = "0";
            }

            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule a Test";
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "NA";
            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.FullName.ToString();

            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();

            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestType.Find(_TestTypeID).Fees.ToString();
                dtpTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "NA";

                _TestAppointment = new clsTestAppointment();
            }

            else
            {
                if (!_LoadTestAppointmentData())
                    return;
            }

            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();

            if (!_HandleActiveTestAppointmentConstraint())
                return;

            if (!_HandledAppointmentLockedConstraint())
                return;

            if (!_HandlePreviousTestConstraint())
                return;


        }

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        private bool _HandleRetakeApplication()
        {
            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.Retake)
            {
                clsApplication Application = new clsApplication();

                Application.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fee;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (!Application.Save())
                {
                    MessageBox.Show("Failed to crete application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;
            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if(_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            MessageBox.Show("Data was not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
