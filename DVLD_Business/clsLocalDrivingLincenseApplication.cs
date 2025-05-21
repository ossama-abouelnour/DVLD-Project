using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsLocalDrivingLincenseApplication : clsApplication
    {
        public enum enMode {AddNew = 0, Update = 1};
        public enMode Mode;
        public int LocalDrivingLicenseApplicationID {  get; set; }

        public int LicenseClassID { get; set; }

        // public clsLicenseClass { get; set; }

        public string FullName
        {
            get
            {
                return base.PersonInfo.FullName;
            }
        }

        public clsLocalDrivingLincenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;
            Mode = enMode.AddNew;

        }

        private clsLocalDrivingLincenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, 
            DateTime ApplicationDate, int applicationTypeID, enApplicationStatus applicationStatus , 
            DateTime lastStatusDate, float paidFees, int createdByUserID, int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationStatus = applicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.LicenseClassID = LicenseClassID;
            //this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            Mode = enMode.Update;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLincenseApplicationData.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);
            return (this.LocalDrivingLicenseApplicationID != -1);
        }
        private bool _UpdateDrivingLicenseApplication()
        {
            return clsLocalDrivingLincenseApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }
        public static clsLocalDrivingLincenseApplication FindByLocalDrivingAppLicenseID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseID = -1;

            if(clsLocalDrivingLincenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseID))
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);
                return new clsLocalDrivingLincenseApplication(LocalDrivingLicenseApplicationID, Application.ApplicationID, 
                    Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, 
                    Application.PaidFees, Application.CreatedByUserID, LicenseID);
            }
            else
                return null;
        }
        public static clsLocalDrivingLincenseApplication FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1, LicenseID = -1;

            if (clsLocalDrivingLincenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID(ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseID))
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);
                return new clsLocalDrivingLincenseApplication(LocalDrivingLicenseApplicationID, Application.ApplicationID, Application.ApplicationDate, 
                    Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID, LicenseID);
            }
            else
                return null;
        }
        public bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;

            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    case enMode.Update:
                    return _UpdateDrivingLicenseApplication();
            }
            return false;
        }
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLincenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public bool Delete()
        {
            bool isLocalDrivingApplicationDeleted = false;
            bool isBaseApplicationDeleted = false;
            isLocalDrivingApplicationDeleted = clsLocalDrivingLincenseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);

            if (!isLocalDrivingApplicationDeleted)
                return false;

            isBaseApplicationDeleted = base.Delete();

            return isLocalDrivingApplicationDeleted;
        }

        public bool DidPassTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLincenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DidPassPreviousTest(clsTestType.enTestType CurrentTestType)
        {
            switch(CurrentTestType)
            {
                case clsTestType.enTestType.EyeTest:
                    return true;

                case clsTestType.enTestType.TheoryTest:
                    return this.DidPassTestType(clsTestType.enTestType.EyeTest);

                    case clsTestType.enTestType.DrivingTest:
                    return this.DidPassTestType(clsTestType.enTestType.TheoryTest);

                    default:
                    return false;
            }
        }

        public bool DidAttendTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLincenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public byte TotalTrialsPerTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLincenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }



    }
}
