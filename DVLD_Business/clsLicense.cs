using DVLD_DataAccess;
using System;
using System.Data;

namespace DVLD_Business
{
    public class clsLicense
    {
        public enum enMode { AddNew = 1, Update = 2 }
        private enMode Mode = enMode.AddNew;

        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 }
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public clsDriver DriverInfo;
        public clsLicenseClass LicenseClassInfo;
        public int DriverID { get; set; }
        public int LicenseClass { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }
        public int CreatedByUserID { get; set; }

        public clsDetainedLicenses DetainedInfo { set; get; }
        public bool IsDetained
        {
            get
            {
                return clsDetainedLicenses.IsLicenseDetained(this.LicenseID);
            }
        }



        public clsLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;

            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }
        public clsLicense(int licenseID, int applicationID, int driverID, int licenseClass, DateTime issueDate, DateTime expirationDate, string notes, decimal paidFees, bool isActive, enIssueReason issueReason, int createdByUserID)
        {
            this.LicenseID = licenseID;
            this.ApplicationID = applicationID;

            this.DriverID = driverID;
            this.DriverInfo = clsDriver.FindDriverByID(driverID);
            this.DetainedInfo = clsDetainedLicenses.FindByLicenseID(this.LicenseID);
            this.LicenseClass = licenseClass;
            this.LicenseClassInfo = clsLicenseClass.Find(licenseClass);
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;
            this.Notes = notes;
            this.PaidFees = paidFees;
            this.IsActive = isActive;
            this.IssueReason = issueReason;
            this.CreatedByUserID = createdByUserID;

            Mode = enMode.Update;
        }
        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);
        }
        public static DataTable GetAllLicenses()
        {
            return clsLicenseData.GetAllLicenses();
            //  clsLicenseData.
        }
        public bool DeactivateCurrentLicense()
        {
            return clsLicenseData.DeactivateLicense(this.LicenseID);
        }

        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1; int DriverID = -1; int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0; bool IsActive = true; int CreatedByUserID = 1;
            byte IssueReason = 1;
            if (clsLicenseData.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass,
            ref IssueDate, ref ExpirationDate, ref Notes,
            ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                                     IssueDate, ExpirationDate, Notes,
                                     PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;

        }
        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate,
                                        this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
            return this.LicenseID != -1;
        }
        private bool _UpdateLicense()
        {
            return clsLicenseData.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate,
                                        this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);

        }

        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIdByPersonID(PersonID, LicenseClassID) != -1);
        }
        public static int GetActiveLicenseIdByPersonID(int PersonID, int LicenseClassID)
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLicense();
            }
            return false;
        }

        public Boolean IsLicenseExpired()
        {
            return this.ExpirationDate < DateTime.Now;
        }
        public static string GetIssueReasonText(enIssueReason reason)
        {
            switch (reason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement for Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement for Lost";

                default:
                    return "First Time";

            }
        }
        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {
            clsApplication Application = new clsApplication();

            Application.PersonID = this.DriverInfo.PersonID;
            Application.Date = DateTime.Now;
            Application.ApplicationTypeID = clsApplication.enApplicationType.RenewDrivingLicense;
            Application.Status = clsApplication.enApplicationStatus.Complete;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.CreatedByUserID = CreatedByUserID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.DriverID = this.DriverInfo.DriverID;
            NewLicense.IsActive = true;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renew;

            if (!NewLicense.Save()) { return null; }

            //we want to deactivate the old License
            DeactivateCurrentLicense();

            return NewLicense;

        }
        public clsLicense ReplaceDamagedOrLostLicense(enIssueReason issueReason, int CreatedByUserID)
        {
            clsApplication Application = new clsApplication();

            Application.PersonID = this.DriverInfo.PersonID;
            Application.Date = DateTime.Now;
            Application.ApplicationTypeID = (issueReason == enIssueReason.DamagedReplacement) ? clsApplication.enApplicationType.ReplaceDamagedDrivingLicense : clsApplication.enApplicationType.ReplaceLostDrivingLicense;
            Application.Status = clsApplication.enApplicationStatus.Complete;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find((int)Application.ApplicationTypeID).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.CreatedByUserID = CreatedByUserID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.DriverID = this.DriverInfo.DriverID;
            NewLicense.IsActive = true;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.Notes = this.Notes;
            NewLicense.IssueReason = issueReason;
            if (!NewLicense.Save()) { return null; }

            //we want to deactivate the old License
            DeactivateCurrentLicense();

            return NewLicense;
        }

        public int Detain(decimal FineFees, int CreatedByUserID)
        {

            clsDetainedLicenses DetainLicense = new clsDetainedLicenses();

            DetainLicense.LicenseID = this.LicenseID;
            DetainLicense.DetainDate = DateTime.Now;
            DetainLicense.CreatedByUserID = CreatedByUserID;
            DetainLicense.FineFees = FineFees;
            if (!DetainLicense.Save())
            {
                return -1;

            }
            return DetainLicense.DetainID;
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {
            clsApplication ReleaseApplication = new clsApplication();

            ReleaseApplication.PersonID = this.DriverInfo.PersonID;
            ReleaseApplication.ApplicationTypeID = clsApplication.enApplicationType.ReleaseDetainedDrivingLicense;
            ReleaseApplication.Date = DateTime.Now;
            ReleaseApplication.Status = clsApplication.enApplicationStatus.Complete;
            ReleaseApplication.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicense).ApplicationFees;
            ReleaseApplication.LastStatusDate = DateTime.Now;
            ReleaseApplication.CreatedByUserID = ReleasedByUserID;

            if (!ReleaseApplication.Save())
            {
                ApplicationID = -1;
                return (ApplicationID != -1);
            }
            ApplicationID = ReleaseApplication.ApplicationID;

            return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID, ReleaseApplication.ApplicationID);
        }

    }
}
