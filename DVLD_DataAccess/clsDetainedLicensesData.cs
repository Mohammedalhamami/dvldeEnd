using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DVLD_DataAccess
{
    public static class clsDetainedLicensesData
    {
        public static bool GetDetainedLicenseInfoByID(int DetainID, ref int LicenseID, ref DateTime DetainDate, ref decimal FineFees,
                ref int CreatedByUserID, ref bool IsReleased, ref DateTime? ReleaseDate, ref int? ReleasedByUserID, ref int? ReleaseApplicationID)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM DetainedLicenses WHERE DetainID=@DetainID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    LicenseID = (int)reader["LicenseID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];
                    //All three below are nullable DataTypes nullable<T>;
                    ReleaseDate = (DateTime?)reader["ReleaseDate"] ?? null;
                    ReleasedByUserID = (int?)reader["ReleasedByUserID"] ?? null;
                    ReleaseApplicationID = (int?)reader["ReleaseApplicationID"] ?? null;

                    IsFound = true;
                    reader.Close();

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
            return IsFound;
        }

        public static bool GetDetainedLicenseInfoByLicenseID(int LicenseID, ref int DetainID, ref DateTime DetainDate, ref decimal FineFees,
               ref int CreatedByUserID, ref bool IsReleased, ref DateTime? ReleaseDate, ref int? ReleasedByUserID, ref int? ReleaseApplicationID)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM DetainedLicenses WHERE LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];

                    ReleaseDate = (DateTime?)reader["ReleaseDate"] ?? null;
                    ReleasedByUserID = (int?)reader["ReleasedByUserID"] ?? null;
                    ReleaseApplicationID = (int?)reader["ReleaseApplicationID"] ?? null;

                    IsFound = true;
                    reader.Close();

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
            return IsFound;
        }

        public static int AddNewDetainLicense(int LicenseID, DateTime DetainDate, decimal FineFees,
               int CreatedByUserID)
        {
            int DetainID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) { 

                SqlCommand command = new SqlCommand("SP_AddNewDetainLicense", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    DetainID = InsertedID;
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
            
            }
            return DetainID;
        }
        public static bool UpdateDetainLicense(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees,
               int CreatedByUserID)
        {
            int AffectedRows = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE DetainedLicenses
                           SET LicenseID = @LicenseID
                              ,DetainDate = @DetainDate
                              ,FineFees = @FineFees
                              ,CreatedByUserID = @CreatedByUserID
                         WHERE DetainID = @DetainID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            try
            {
                connection.Open();

                AffectedRows = command.ExecuteNonQuery();



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
            return (AffectedRows > 0);
        }
        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM DetainedLicenses_View ORDER BY IsReleased, DetainID";

            SqlCommand command = new SqlCommand(query, connection);

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
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public static bool ReleaseDetainedLicense(int DetainID, int? ReleasedByUserID, int? ReleaseApplicationID)
        {
            int AffectedRows = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE dbo.DetainedLicenses
                              SET IsReleased = 1, 
                              ReleaseDate = @ReleaseDate, 
                              ReleaseApplicationID = @ReleaseApplicationID   
                              WHERE DetainID=@DetainID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);


            try
            {
                connection.Open();

                AffectedRows = command.ExecuteNonQuery();

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
            return (AffectedRows > 0);
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsDetained = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT IsDetained=1 FROM DetainedLicenses WHERE LicenseID=@LicenseID AND IsReleased=0";

            SqlCommand command = new SqlCommand(@query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsDetained = Convert.ToBoolean(result);
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

            return IsDetained;
        }
    }
}
