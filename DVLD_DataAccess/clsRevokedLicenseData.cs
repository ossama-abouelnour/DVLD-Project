using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsRevokedLicenseData
    {
        public static bool IsRevokedLicense(int LicenseID)
        {
            bool isRevoked = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select IsRevoked=1 
                            FROM detainedLicenses 
                            WHERE 
                            LicenseID=@LicenseID 
                            and IsReleased=0;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null) 
                {
                    isRevoked = Convert.ToBoolean(result);
                }

            }
            catch (Exception ex) 
            {
                
            }

            finally
            {
                connection.Close();
            }
            return isRevoked;
        }
    }
}
