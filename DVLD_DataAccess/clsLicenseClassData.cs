using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLicenseClassData
    {
        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM LicenseClasses ORDER BY ClassName;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    dt.Load(reader);
                }


                reader.Close();
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
        public static bool GetLicenseClassInfoByID(int LicenseClassID, ref string ClassName, ref string ClassDescription, ref byte MinAllowedAge, ref byte DefaultValidityLength, ref float Fees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ClassName = Convert.ToString(reader["ClassName"]);
                    ClassDescription = Convert.ToString(reader["ClassDescription"]);
                    MinAllowedAge = Convert.ToByte(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToByte(reader["DefaultValidityLength"]);
                    Fees = Convert.ToSingle(reader["ClassFees"]);
                }


                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool GetLicenseClassInfoByName(string ClassName, ref int LicenseClassID, ref string ClassDescription, ref byte MinAllowedAge, ref byte DefaultValidityLength, ref float Fees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM LicenseClasses WHERE ClassName = @ClassName;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    ClassDescription = Convert.ToString(reader["ClassDescription"]);
                    MinAllowedAge = Convert.ToByte(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToByte(reader["DefaultValidityLength"]);
                    Fees = Convert.ToSingle(reader["ClassFees"]);
                }


                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static int AddNewLicenseClass(string ClassName, string ClassDescription,
            byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees)
        {
            int LicenseClassID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert Into LicenseClasses 
                            (ClassName,ClassDescription,MinimumAllowedAge, 
                             DefaultValidityLength,ClassFees)
                             Values (@ClassName,@ClassDescription,@MinimumAllowedAge, 
                             @DefaultValidityLength,@ClassFees)
                             where LicenseClassID = @LicenseClassID;
                             SELECT SCOPE_IDENTITY();";



            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);



            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseClassID = insertedID;
                }
            }

            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }


            return LicenseClassID;

        }
        public static bool UpdateLicenseClass(int LicenseClassID, string ClassName,
            string ClassDescription,
            byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  LicenseClasses  
                            set ClassName = @ClassName,
                                ClassDescription = @ClassDescription,
                                MinimumAllowedAge = @MinimumAllowedAge,
                                DefaultValidityLength = @DefaultValidityLength,
                                ClassFees = @ClassFees
                                where LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
               
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

    }
}
