using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enApplicationType
        {
            NewDrivingLicence = 1, RenewDrivingLicence = 2, ReplaceLostDrivingLicence = 3,
            ReplaceDamagedDrivingLicence = 4, ReleaseRevokedLicence = 5, NewInternationalLicence = 6, RetakeTest = 7
        };

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };

        public enApplicationStatus ApplicationStatus { get; set; }

        public clsUser CreatedByUserInfo;
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }

        public clsPerson PersonInfo { get; set; }
        public string ApplicantFullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName;
            }
        }

        public DateTime ApplicationDate { get; set; }

        public int ApplicationTypeID { get; set; }

        public clsApplicationType ApplicationTypeInfo;

        public string StatusText
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";

                    case enApplicationStatus.Cancelled:
                        return "Cancelled";

                    case enApplicationStatus.Completed:
                        return "Completed";

                    default:
                        return "Unknown";
                }
            }
        }

        public DateTime LastStatusDate { get; set; }

        public float PaidFees { get; set; }

        public int CreatedByUserID { get; set; }

        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private clsApplication(enApplicationStatus applicationStatus, int applicationID, int applicantPersonID, 
            DateTime applicationDate, int applicationTypeID, DateTime lastStatusDate, float paidFees, int createdByUserID)
        {
            this.Mode = enMode.Update;
            this.ApplicationStatus = applicationStatus;

            this.CreatedByUserInfo = clsUser.FindByUserID(createdByUserID);
            this.ApplicationID = applicationID;
            this.PersonInfo = clsPerson.Find(applicantPersonID);

            this.ApplicantPersonID = applicantPersonID;
            this.ApplicationDate = applicationDate;
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Find(applicationTypeID);
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }

        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, 2);
        }
        public bool SetComplete()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, 3);
        }

        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            bool isFound = false;
            int ApplicationPersonID = -1; DateTime ApplicationDate = DateTime.Now; int ApplicationTypeID = -1; byte ApplicationStatus = 1; DateTime LastStatusDate = DateTime.Now; float PaidFees = 0; int CreatedByUserID = -1;

            isFound = clsApplicationData.GetApplicationInfoByID(ApplicationID, ref ApplicationPersonID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID);

            if (isFound)
            {
                return new clsApplication((enApplicationStatus)ApplicationStatus, ApplicationID, ApplicationPersonID, ApplicationDate, ApplicationTypeID, LastStatusDate, PaidFees, CreatedByUserID);
            }

            else
                return null;
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if(_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                        
                    else
                        return false;

                case enMode.Update:
                       return _UpdateApplication();
            }
            return false;
        }

        public bool Delete()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(PersonID, (int)ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int ApplicationID, enApplicationType ApplicationTypeID )
        {
            return clsApplicationData.GetActiveApplicationID(ApplicationID, (int)ApplicationTypeID);
        }

        public int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }
    }
        
}
