﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DVLD_DataAccess
{
    public static class clsPersonData
    {
        public static bool GetPersonInfoByID(int PersonID, ref string NationalNo, ref string FirstName,
                  ref string SecondName, ref string ThirdName, ref string LastName,
                  ref DateTime DateOfBirth, ref byte Gender, ref string Address,
                  ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM People WHERE PersonID=@PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    //The record was found
                    isFound = true;

                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    //ThirdName allows null in database so we should handle null;
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (byte)reader["Gender"];
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

                    NationalityCountryID = (int)reader["NationalityCountryID"];

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
            catch (Exception e)
            {
                if (!EventLog.SourceExists("dvldEnd"))
                {
                    EventLog.CreateEventSource("dvldEnd", "Application");
                    EventLog.WriteEntry("dvldEnd", e.Message, EventLogEntryType.Error);
                }
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }


        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int PersonID, ref string FirstName,
                      ref string SecondName, ref string ThirdName, ref string LastName,
                      ref DateTime DateOfBirth, ref byte Gender, ref string Address,
                      ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM People WHERE NationalNo=@NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    //The record was found
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    //ThirdName allows null in database so we should handle null;
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (byte)reader["Gender"];
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

                    NationalityCountryID = (int)reader["NationalityCountryID"];

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
            catch (Exception e)
            {
                if (!EventLog.SourceExists("dvldEnd"))
                {
                    EventLog.CreateEventSource("dvldEnd", "Application");
                    EventLog.WriteEntry("dvldEnd", e.Message, EventLogEntryType.Error);
                }
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }


        public static int AddNewPerson(string NationalNo, string FirstName,
                  string SecondName, string ThirdName, string LastName,
                  DateTime DateOfBirth, byte Gender, string Address,
                  string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int PersonID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                SqlCommand command = new SqlCommand("SP_AddNewPerson", connection);
                //user stored procedure insted of hard query.
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@NationalNo", NationalNo);
                command.Parameters.AddWithValue("@FirstName", FirstName);
                command.Parameters.AddWithValue("@SecondName", SecondName);

                if (ThirdName != "" && ThirdName != null)
                {
                    command.Parameters.AddWithValue("@ThirdName", ThirdName);
                }
                else
                {
                    command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
                }

                command.Parameters.AddWithValue("@LastName", LastName);
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

                    object Result = command.ExecuteScalar();

                    if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                    {
                        PersonID = InsertedID;
                    }

                }
                catch (Exception e)
                {
                    if (!EventLog.SourceExists("dvldEnd"))
                    {
                        EventLog.CreateEventSource("dvldEnd", "Application");
                        EventLog.WriteEntry("dvldEnd", e.Message, EventLogEntryType.Error);
                    }
                }

                return PersonID;
            }


        }


        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName,
                      string SecondName, string ThirdName, string LastName,
                      DateTime DateOfBirth, byte Gender, string Address,
                      string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                SqlCommand command = new SqlCommand("SP_UpdatePerson", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@NationalNo", NationalNo);
                command.Parameters.AddWithValue("@FirstName", FirstName);
                command.Parameters.AddWithValue("@SecondName", SecondName);

                if (ThirdName != "" && ThirdName != null)
                {
                    command.Parameters.AddWithValue("@ThirdName", ThirdName);
                }
                else
                {
                    command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
                }

                command.Parameters.AddWithValue("@LastName", LastName);
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
                catch (Exception e) { if (!EventLog.SourceExists("dvldEnd")) { EventLog.CreateEventSource("dvldEnd", "Application"); EventLog.WriteEntry("dvldEnd", e.Message, EventLogEntryType.Error); } }
            }
            return (rowsAffected > 0);
        }

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                SqlCommand command = new SqlCommand("SP_GetAllPeople", connection);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                    reader.Close();
                }
                catch (Exception e) { if (!EventLog.SourceExists("dvldEnd")) { EventLog.CreateEventSource("dvldEnd", "Application"); EventLog.WriteEntry("dvldEnd", e.Message, EventLogEntryType.Error); } }

            }
            return dt;
        }

        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                SqlCommand command = new SqlCommand("SP_DeletePerson", connection);
                command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception e) { if (!EventLog.SourceExists("dvldEnd")) { EventLog.CreateEventSource("dvldEnd", "Application"); EventLog.WriteEntry("dvldEnd", e.Message, EventLogEntryType.Error); } }
            
            }
            return (rowsAffected > 0);
        }

        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                isFound = (result != null);
            }
            catch (Exception e) { if (!EventLog.SourceExists("dvldEnd")) { EventLog.CreateEventSource("dvldEnd", "Application"); EventLog.WriteEntry("dvldEnd", e.Message, EventLogEntryType.Error); } }
            finally { connection.Close(); }
            return isFound;
        }

        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                isFound = (result != null);
            }
            catch (Exception e) { if (!EventLog.SourceExists("dvldEnd")) { EventLog.CreateEventSource("dvldEnd", "Application"); EventLog.WriteEntry("dvldEnd", e.Message, EventLogEntryType.Error); } }
            finally { connection.Close(); }
            return isFound;
        }
    }

}
