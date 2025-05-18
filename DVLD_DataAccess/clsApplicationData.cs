﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess
{
    public class clsApplicationData
    {
        public static bool GetApplicationInfoByID(int ApplicationID, ref int ApplicantPersonID, 
            ref DateTime ApplicationDate, ref int ApplicationTypeID, ref byte ApplicationStatus, 
            ref DateTime LastStatusDate, ref float PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);



            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicantPersonID = (int)(reader["ApplicantPersonID"]);
                    ApplicationDate = (DateTime)(reader["ApplicationDate"]);
                    ApplicationTypeID = (int)(reader["ApplicationTypeID"]);
                    ApplicationStatus = (byte)(reader["ApplicationStatus"]);
                    LastStatusDate = (DateTime)(reader["LastStatusDate"]);
                    PaidFees = (float)(reader["PaidFees"]);
                    CreatedByUserID = (int)(reader["CreatedByUserID"]);

                }
                else
                    isFound = false;

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

        public static DataTable GetAllApplications()
        {
            DataTable dt =  new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Applications ORDER BY ApplicationTypeTitle;";

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

        public static int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, 
            byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int ApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO Applications 
                             (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
                             VALUES
                             (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);



            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ApplicationID = insertedID;
                }

            }
            catch (Exception ex)
            {
                ApplicationID = -1;
            }
            finally
            {
                connection.Close();
            }

            return ApplicationID;
        }

        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, 
            int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE Applications 
                             SET
                             ApplicantPersonID = @ApplicantPersonID,
                             ApplicationDate = @ApplicationDate,
                             ApplicationTypeID = @ApplicationTypeID,
                             ApplicationStatus = @ApplicationStatus,
                             LastStatusDate = @LastStatusDate,
                             PaidFees = @PaidFees,
                             CreatedByUserID = @CreatedByUserID
                             WHERE ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);



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

        public static bool DeleteApplication(int ApplicationID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"DELETE Applications 
                             WHERE ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

        public static bool IsApplicationExist(int ApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT FOUND = 1 FROM Applications 
                             WHERE ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;

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

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != -1);
        }

        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT ActiveApplicationID = ApplicationID FROM Applications 
                             WHERE ApplicantPersonID = @ApplicantPersonID and ApplicationTypeID = @ApplicationTypeID and ApplicationStatus=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    ActiveApplicationID = AppID;
                }

            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT ActiveApplicationID=Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID=@ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    ActiveApplicationID = AppID;
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }

        public static bool UpdateStatus(int ApplicationID, byte NewStatus)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE Applications 
                             SET
                             ApplicationStatus = @NewStatus,
                             LastStatusDate = @LastStatusDate
                             WHERE ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicationStatus", NewStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);




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
