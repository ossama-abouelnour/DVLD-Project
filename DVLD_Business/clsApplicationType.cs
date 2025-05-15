using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsApplicationType
    {
        public enum enMode {AddNew = 0, Update = 1}

        public enMode Mode = enMode.AddNew;

        public int ID {  get; set; }

        public string Title { get; set; }

        public float Fee { get; set; }

        public clsApplicationType()
        {
            this.ID = -1;
            this.Title = "";
            this.Fee = 0;
            this.Mode = enMode.AddNew;
        }

        public clsApplicationType(int ID, string ApplicationTitle, float ApplicationFee)
        {
            this.ID = ID;
            this.Title = ApplicationTitle;
            this.Fee = ApplicationFee;
        }

        private bool _AddNewApplicationType()
        {
            this.ID = clsApplicationTypeData.AddNewApplicationType(this.Title, this.Fee);
            return (this.ID != -1);
        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypeData.UpdateApplicationType(this.ID, this.Title, this.Fee);
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    break;
                    case enMode.Update:
                    return _UpdateApplicationType();

                default:
                    return false;
            }
            return false;
        }


    }
}
