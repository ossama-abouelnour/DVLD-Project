using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsPersonData
    {
        public static bool GetPersonInfoByID(int PersonID, ref string FirstName, ref string MiddleName, ref string LastName, ref string NationalNo, ref DateTime DateOfBirth, ref short Gender, ref string Address,
            ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    MiddleName = (string)reader["MiddleName"];
                    LastName = (string)reader["LastName"];
                    NationalNo = (string)reader["NationalNo"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = Convert.ToInt16(reader["Gender"]);
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }

                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }

                    else 
                    {
                        ImagePath = "";
                    }
                    
                }

                else 
                {
                    isFound = false;
                }
                reader.Close();
                
            }
            catch (Exception ex)
            {
                
            }
            finally 
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string MiddleName, ref string LastName, ref DateTime DateOfBirth, ref short Gender, ref string Address,
            ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    MiddleName = (string)reader["MiddleName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = Convert.ToInt16(reader["Gender"]);
                    Address =  reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = reader["Email"].ToString();
                    }
                    else
                    {
                        Email = "";
                    }

                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = reader["ImagePath"].ToString();
                    }

                    else
                    {
                        ImagePath = "";
                    }

                }

                else
                {
                    isFound = false;
                }
                reader.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static int AddNewPerson(string FirstName, string MiddleName, string LastName, string NationalNo, DateTime DateOfBirth, short Gender, string Address,
            string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO People(FirstName, MiddleName, LastName, NationalNo, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath)
                           VALUES (@FirstName, @MiddleName, @LastName, @NationalNo, @DateOfBirth, @Gender, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);
                           SELECT SCOPE_IDENTITY();
                        ";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@MiddleName", MiddleName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if(Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

                try
                {
                    connection.Open();
                    
                    object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    PersonID = InsertedID;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
            return PersonID;
        }

        public static bool UpdatePerson(int PersonID, string FirstName, string MiddleName, string LastName, string NationalNo, DateTime DateOfBirth, short Gender, string Address,
            string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update People
                            SET 
                             FirstName = @FirstName,
                             MiddleName = @MiddleName,
                             LastName = @LastName,
                             NationalNo = @NationalNo,
                             DateOfBirth = @DateOfBirth,
                             Gender = @Gender,
                             Address = @Address,
                             Phone = @Phone,
                             Email = @Email,
                             NationalityCountryID = @NationalityCountryID,
                             ImagePath = @ImagePath
                             WHERE PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@MiddleName", MiddleName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

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

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT PersonID, NationalNo,
                            FirstName, MiddleName, LastName,
                            DateOfBirth, Gender,  
                            CASE
                            WHEN People.Gender = 0 THEN 'Male'
                            ELSE 'Female'
                            END as GenderCaption,
                            Address, Phone, Email, 
                            NationalityCountryID, CountryName, ImagePath
                            FROM People
                            INNER JOIN Countries ON People.NationalityCountryID = Countries.CountryID
                            ORDER BY People.FirstName;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"DELETE People
                            WHERE PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("PersonID", PersonID);

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

        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection( clsDataAccessSettings.ConnectionString);

            string query = "SELECT IsFound = TRUE WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
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

        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT IsFound = TRUE WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
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

    }
}
