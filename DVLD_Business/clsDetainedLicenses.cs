
using DVLD_DataAccess;
using System;
using System.Data;

namespace DVLD_Business
{
    public class clsDetainedLicenses
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode Mode = enMode.AddNew;
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }
        public bool IsReleased { get; set; }
        //The ReleaseDate prop. whith ? is nullable<DateTime> datatype.
        public DateTime? ReleaseDate { get; set; }
        //it is nullable with ?.
        public int? ReleasedByUserID { get; set; }
        public clsUser ReleasedByUserInfo { get; set; }
        public int? ReleaseApplicationID { get; set; }

        public clsDetainedLicenses()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = null;
            this.ReleasedByUserID = null;
            this.ReleaseApplicationID = null;
            Mode = enMode.AddNew;
        }
        public clsDetainedLicenses(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)
        {

            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(this.CreatedByUserID);
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            /*Bcuz .FindByUserID parameter datatype int, i have to do this exp to check if
             * ReleasedByUserID has num or null ? if null return -1;
            */
            this.ReleasedByUserInfo = clsUser.FindByUserID(this.ReleasedByUserID ?? -1);
            this.ReleaseApplicationID = ReleaseApplicationID;

            Mode = enMode.Update;
        }

        public static clsDetainedLicenses FindByID(int DetainID)
        {
            int LicenseID = -1, CreatedByUserID = -1;
            int? ReleasedByUserID = null, ReleaseApplicationID = null;
            DateTime DetainDate = DateTime.MinValue;
            DateTime? ReleaseDate = null;
            bool IsReleased = false;
            decimal FineFees = 0;
            if (clsDetainedLicensesData.GetDetainedLicenseInfoByID(DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
            {
                return null;
            }
        }
        public static clsDetainedLicenses FindByLicenseID(int LicenseID)
        {
            int DetainID = -1, CreatedByUserID = -1;
            int? ReleasedByUserID = null, ReleaseApplicationID = null;
            DateTime DetainDate = DateTime.MinValue;
            DateTime? ReleaseDate = null;
            bool IsReleased = false;
            decimal FineFees = 0;
            if (clsDetainedLicensesData.GetDetainedLicenseInfoByLicenseID(LicenseID, ref DetainID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
            {
                return null;
            }
        }
        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicensesData.GetAllDetainedLicenses();
        }
        private bool _AddNewDetainedLicense()
        {

            this.DetainID = clsDetainedLicensesData.AddNewDetainLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
            return (this.DetainID != -1);
        }
        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicensesData.UpdateDetainLicense(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
        }
        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (this._AddNewDetainedLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return (this._UpdateDetainedLicense());
            }
            return false;
        }
        public bool ReleaseDetainedLicense(int? ReleasedByUserID, int? ReleaseApplicationID)
        {
            return clsDetainedLicensesData.ReleaseDetainedLicense(this.DetainID, ReleasedByUserID, ReleaseApplicationID);
        }
        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicensesData.IsLicenseDetained(LicenseID);
        }
    }
}
