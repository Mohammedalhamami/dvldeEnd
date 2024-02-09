using DVLD_DataAccess;
using System;
using System.Data;

namespace DVLD_Business
{
    public class clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2,
            ReplaceDamagedDrivingLicense = 3, ReplaceLostDrivingLicense = 4, ReleaseDetainedDrivingLicense = 5,
            NewInternationalLicense = 6, RetakeTest = 7
        };
        public enum enApplicationStatus { New = 1, Cancelled = 2, Complete = 3 };

        public enMode Mode = enMode.AddNew;
        public int ApplicationID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public string ApplicantFullName
        {
            get
            {
                return clsPerson.Find(this.PersonID).FullName;
            }
        }
        public DateTime Date { get; set; }
        public clsApplication.enApplicationType ApplicationTypeID { get; set; }
        public clsApplicationType ApplicationTypeInfo;
        public enApplicationStatus Status { get; set; }
        public string StatusText
        {
            get
            {
                switch (this.Status)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Complete:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }
        }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;
        public clsApplication()
        {
            this.ApplicationID = -1;
            this.PersonID = -1;
            this.Date = DateTime.Now;
            this.Status = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
        }
        public clsApplication(int iD, int personID, DateTime date, clsApplication.enApplicationType ApplicationTypeID, enApplicationStatus status, DateTime lastStatusDate, decimal paidFees, int createdByUserID)
        {
            this.ApplicationID = iD;
            this.PersonID = personID;
            this.PersonInfo = clsPerson.Find(personID);
            // this.ApplicantFullName = clsPerson.Find(personID).FullName;
            this.Date = date;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Find((int)ApplicationTypeID);
            this.Status = status;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(createdByUserID);
            Mode = enMode.Update;
        }
        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            DateTime Date = DateTime.Today, LastStatusDate = DateTime.Today;
            int PersonID = -1, ApplicationTypeID = -1, CreatedByUserID = -1;
            decimal PaidFees = 0;
            byte ApplicationStatus = 1;
            if (clsApplicationData.GetApplicationInfoByID(ApplicationID, ref PersonID, ref Date, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID, PersonID, Date, (clsApplication.enApplicationType)ApplicationTypeID, (clsApplication.enApplicationStatus)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            }
            return null;
        }
        public static DataTable GetAllApplications()
        {
            return clsApplicationData.GetAllApplications();
        }
        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(this.PersonID, this.Date, (int)this.ApplicationTypeID, (byte)this.Status, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
            return (ApplicationID != -1);
        }
        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID, this.PersonID, this.Date, (int)this.ApplicationTypeID, (byte)this.Status, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }
        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return (_UpdateApplication());
            }
            return false;
        }
        public bool Delete()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }
        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);
        }
        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, 2);
        }
        public bool SetComplete()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, 3);
        }
        public static bool DoesePersonHasActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHasActiveApplication(PersonID, ApplicationTypeID);
        }
        public bool DoesePersonHasActiveApplication(int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHasActiveApplication(this.PersonID, ApplicationTypeID);
        }
        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }
        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }
        public int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.PersonID, ApplicationTypeID);
        }
    }
}
