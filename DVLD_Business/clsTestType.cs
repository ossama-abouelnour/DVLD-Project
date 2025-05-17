using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTestType
    {
        public enum enMode {AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public enum enTestType { EyeTest = 1, TheoryTest = 2, DrivingTest = 3 }

        public clsTestType.enTestType TestTypeID {  get; set; }
        public string TestTypeTitle { get; set;}
        public string TestTypeDescription { get; set; }
        public float Fees { get; set; }

        clsTestType()
        {
            this.TestTypeID = clsTestType.enTestType.EyeTest;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.Fees = 0;
            this.Mode = enMode.AddNew;
        }

        clsTestType(clsTestType.enTestType TestTypeID, string TestTypeTitle, string TestTypeDescription, float Fees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.Fees = Fees;
            Mode = enMode.Update;
        }

        private bool _AddNewTestType()
        {
            this.TestTypeID = (clsTestType.enTestType)clsTestTypeData.AddNewTestType(this.TestTypeTitle, this.TestTypeDescription, this.Fees);

            return (this.TestTypeTitle != "");
        }

        private bool _UpdateTestType()
        {
            return clsTestTypeData.UpdateTestType((int)this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.Fees);
        }

        public static clsTestType Find(clsTestType.enTestType TestTypeID)
        {
            string TestTypeTitle = "", TestTypeDescription = "";
            float Fees = 0;

            if (clsTestTypeData.GetTestTypeInfoByID((int)TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref Fees))
            {
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, Fees);
            }
            return null;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateTestType();
            }
            return false;
        }

    }
}
