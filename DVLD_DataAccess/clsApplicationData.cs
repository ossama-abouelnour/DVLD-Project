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
    }
}
