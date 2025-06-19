using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsRevokedLicense
    {

        public static bool IsRevoked(int LicenseID)
        {
            return clsRevokedLicenseData.IsRevokedLicense(LicenseID);
        }
    }
}
