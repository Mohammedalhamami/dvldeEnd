using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DVLD_DataAccess
{
    public class clsDriverData
    {
        public static bool GetDriverInfoByDriverID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) { 

                SqlCommand command = new SqlCommand("SP_GetDriverInfoByDriverID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                    IsFound = true;
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
            }
            return IsFound;
        }
        public static bool GetDriverInfoByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Drivers where PersonID=@PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    DriverID = (int)reader["DriverID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                    IsFound = true;
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

            finally { connection.Close(); }
            return IsFound;
        }

        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

            SqlCommand command = new SqlCommand("SP_GetAllDrivers", connection);
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
            catch (Exception e)
            {
                if (!EventLog.SourceExists("dvldEnd"))
                {
                    EventLog.CreateEventSource("dvldEnd", "Application");
                    EventLog.WriteEntry("dvldEnd", e.Message, EventLogEntryType.Error);
                }
            }
           
            }
            return dt;
        }
        public static int AddNewDriver(int PersonID, int CreatedByUserID)
        {
            int DriverID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Drivers
           (PersonID
           ,CreatedByUserID
           ,CreatedDate)
     VALUES
           (@PersonID
           ,@CreatedByUserID
           ,@CreatedDate)
     SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ReturnedID))
                {
                    DriverID = ReturnedID;
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
            finally { connection.Close(); }
            return DriverID;
        }

        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID)
        {
            bool IsUpdated = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Drivers
                           SET PersonID=@PersonID
                              ,CreatedByUserID=@CreatedByUserID
                              ,CreatedDate=@CreatedDate
                         WHERE DriverID=@DriverID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

            try
            {
                connection.Open();
                int AffectedRows = command.ExecuteNonQuery();
                IsUpdated = (AffectedRows > 0);
            }
            catch (Exception e)
            {
                if (!EventLog.SourceExists("dvldEnd"))
                {
                    EventLog.CreateEventSource("dvldEnd", "Application");
                    EventLog.WriteEntry("dvldEnd", e.Message, EventLogEntryType.Error);
                }
            }
            finally { connection.Close(); }
            return IsUpdated;
        }

    }
}
