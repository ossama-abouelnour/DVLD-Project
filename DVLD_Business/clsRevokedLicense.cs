using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsRevokedLicense
    {
        public enum enMode {AddNew = 0, Update = 1};
        public enMode Mode = enMode.AddNew;

        public int RevokeID {  get; set; }
        public int LicenseID { get; set; }
        public DateTime RevokeDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public clsUser ReleasedByUserInfo { get; set; }
        public int ReleaseApplicationID { get; set; }

        public clsRevokedLicense()
        {
            this.RevokeID = -1;
            this.LicenseID = -1;
            this.RevokeDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.MaxValue;
            this.ReleasedByUserID = 0;
            this.ReleaseApplicationID = -1;



            Mode = enMode.AddNew;
        }

        public clsRevokedLicense(int RevokeID,
            int LicenseID, DateTime RevokeDate,
            float FineFees, int CreatedByUserID,
            bool IsReleased, DateTime ReleaseDate,
            int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.RevokeID = RevokeID;
            this.LicenseID = LicenseID;
            this.RevokeDate = RevokeDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(this.CreatedByUserID);
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.ReleasedByUserInfo = clsUser.FindByPersonID(this.ReleasedByUserID);
            Mode = enMode.Update;
        }

        public static bool IsRevoked(int LicenseID)
        {
            return clsRevokedLicenseData.IsRevokedLicense(LicenseID);
        }

        private bool _AddNewDetainedLicense()
        {

            this.RevokeID = clsRevokedLicenseData.AddNewRevokedLicense(
                this.LicenseID, this.RevokeDate, this.FineFees, this.CreatedByUserID);

            return (this.RevokeID != -1);
        }

        private bool _UpdateDetainedLicense()
        {

            return clsRevokedLicenseData.UpdateRevokedLicense(
                this.RevokeID, this.LicenseID, this.RevokeDate, this.FineFees, this.CreatedByUserID);
        }

        public static clsRevokedLicense Find(int DetainID)
        {
            int LicenseID = -1; DateTime DetainDate = DateTime.Now;
            float FineFees = 0; int CreatedByUserID = -1;
            bool IsReleased = false; DateTime ReleaseDate = DateTime.MaxValue;
            int ReleasedByUserID = -1; int ReleaseApplicationID = -1;

            if (clsRevokedLicenseData.GetDetainedLicenseInfoByLicenseID(DetainID,
            ref LicenseID, ref DetainDate,
            ref FineFees, ref CreatedByUserID,
            ref IsReleased, ref ReleaseDate,
            ref ReleasedByUserID, ref ReleaseApplicationID))

                return new clsRevokedLicense(DetainID,
                     LicenseID, DetainDate,
                     FineFees, CreatedByUserID,
                     IsReleased, ReleaseDate,
                     ReleasedByUserID, ReleaseApplicationID);
            else
                return null;

        }

        public static DataTable GetAllDetainedLicenses()
        {
            return clsRevokedLicenseData.GetAllRevokedLicenses();

        }

        public static clsRevokedLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1; DateTime DetainDate = DateTime.Now;
            float FineFees = 0; int CreatedByUserID = -1;
            bool IsReleased = false; DateTime ReleaseDate = DateTime.MaxValue;
            int ReleasedByUserID = -1; int ReleaseApplicationID = -1;

            if (clsRevokedLicenseData.GetDetainedLicenseInfoByLicenseID(LicenseID,
            ref DetainID, ref DetainDate,
            ref FineFees, ref CreatedByUserID,
            ref IsReleased, ref ReleaseDate,
            ref ReleasedByUserID, ref ReleaseApplicationID))

                return new clsRevokedLicense(DetainID,
                     LicenseID, DetainDate,
                     FineFees, CreatedByUserID,
                     IsReleased, ReleaseDate,
                     ReleasedByUserID, ReleaseApplicationID);
            else
                return null;

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDetainedLicense();

            }

            return false;
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {
            return clsRevokedLicenseData.ReleaseRevokedLicense(this.RevokeID,
                   ReleasedByUserID, ReleaseApplicationID);
        }
    }
}

