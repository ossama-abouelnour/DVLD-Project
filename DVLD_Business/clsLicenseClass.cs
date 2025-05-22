using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsLicenseClass
    {
        public enum enMode {AddNew = 0, Update = 1 }
        public enMode Mode;

        public int LicenseClassID {  get; set; }
        public string ClassName {  get; set; }
        public string ClassDescription { get; set; }

        public byte MinimumAgeAllowed { get; set; }

        public byte DefaultValidityLength { get; set; }

        public float ClassFees { get; set; }

        public clsLicenseClass()
        {
            Mode = enMode.AddNew;
            this.LicenseClassID = 0;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAgeAllowed = 0;
            this.DefaultValidityLength = 0;
            this.ClassFees = 0;
        }

        private clsLicenseClass(int licenseClassID, string className, string classDescription, byte minimumAgeAllowed, byte defaultValidityLength, float classFees)
        {
            this.Mode = enMode.Update;
            this.LicenseClassID = licenseClassID;
            this.ClassName = className;
            this.ClassDescription = classDescription;
            this.MinimumAgeAllowed = minimumAgeAllowed;
            this.DefaultValidityLength = defaultValidityLength;
            this.ClassFees = classFees;
        }

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }
    }
}
