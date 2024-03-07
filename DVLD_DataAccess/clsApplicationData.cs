using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DVLD_DataAccess
{
    public class clsApplicationData
    {
        public static bool GetApplicationInfoByID(int ID, ref int PersonID, ref DateTime Date, ref int TypeID
                                                  , ref byte Status, ref DateTime LastStatusDate,
                                                   ref decimal PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = "SELECT * FROM Applications WHERE ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    PersonID = (int)reader["ApplicantPersonID"];
                    Date = (DateTime)reader["ApplicationDate"];
                    TypeID = (int)reader["ApplicationTypeID"];
                    Status = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally { connection.Close(); }


            return isFound;
        }

        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                string query = @"SELECT * FROM Applications ORDER BY ApplicationID";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                        dt.Load(reader);
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
        public static int AddNewApplication(int PersonID, DateTime Date, int TypeID
                                                  , byte Status, DateTime LastStatusDate,
                                                   decimal PaidFees, int CreatedByUserID)
        {
            int ApplicationID = -1;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

            SqlCommand command = new SqlCommand("SP_AddNewApplication", connection);
                command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationDate", Date);
            command.Parameters.AddWithValue("@ApplicationTypeID", TypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", Status);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            try
            {
                connection.Open();

                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    ApplicationID = InsertedID;
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
            return ApplicationID;
        }
        public static bool UpdateApplication(int ID, int PersonID, DateTime Date, int TypeID
                                                  , byte Status, DateTime LastStatusDate,
                                                   decimal PaidFees, int CreatedByUserID)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Applications
                             SET ApplicantPersonID=@ApplicantPersonID, ApplicationDate=@ApplicationDate
                             , ApplicationTypeID=@ApplicationTypeID, ApplicationStatus=@ApplicationStatus, 
                             LastStatusDate=@LastStatusDate, PaidFees=@PaidFees, CreatedByUserID=@CreatedByUserID
                             WHERE ApplicationID=@ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantID", ID);
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationDate", Date);
            command.Parameters.AddWithValue("@ApplicationTypeID", TypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", Status);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                int RowsAffcted = command.ExecuteNonQuery();

                isUpdated = (RowsAffcted > 0);
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
            return isUpdated;
        }
        public static bool DeleteApplication(int ID)
        {
            bool IsDeleted = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE Applications WHERE ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ID);

            try
            {
                connection.Open();
                int RowsAffected = command.ExecuteNonQuery();

                IsDeleted = (RowsAffected > 0);

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
            return IsDeleted;

        }
        public static bool IsApplicationExist(int ID)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT FOUND=1 FROM Applications WHERE ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                IsExist = (result != null);
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
            return IsExist;
        }
        public static bool DoesPersonHasActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != -1);
        }
        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT ActiveApplicationID=ApplicationID FROM Applications WHERE ApplicantPersonID=@ApplicantPersonID AND ApplicationTypeID=@ApplicationTypeID AND ApplicationStatus=1 ;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    ActiveApplicationID = InsertedID;
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
            return ActiveApplicationID;
        }
        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT ActiveApplicationID = Applications.ApplicationID
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID
                            and ApplicationTypeID = @ApplicationTypeID
                            and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus = 1";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    ActiveApplicationID = InsertedID;
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
            return ActiveApplicationID;
        }
        public static bool UpdateStatus(int ApplicationID, byte NewStatus)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Applications
                             SET ApplicationStatus=@ApplicationStatus, LastStatusDate=@LastStatusDate
                             WHERE ApplicationID=@ApplicationID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationStatus", NewStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();

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
            return RowsAffected > 0;
        }
    }
}
