using DVLD_DataAccess;
using System;
using System.Data;

namespace DVLD_Business
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 };

        public enMode Mode = enMode.AddNew;



        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }

        }
        public string NationalNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public clsCountry CountryInfo;
        private string _ImagePath;
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }

        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";

            this.NationalNo = "";
            this.DateOfBirth = DateTime.Now;

            this.Phone = "";
            this.Email = "";
            this.Address = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";
            this.CountryInfo = null;
            Mode = enMode.AddNew;
        }

        private clsPerson(int personID, string firstName, string secondName,
            string thirdName, string lastName, string nationalNo, DateTime dateOfBirth
            , byte gender, string address, string phone, string email, int nationalityCountryID, string imagePath)
        {

            this.PersonID = personID;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.NationalNo = nationalNo;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.NationalityCountryID = nationalityCountryID;
            this.CountryInfo = clsCountry.Find(nationalityCountryID);
            this.ImagePath = imagePath;
            this.Mode = enMode.Update;

        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.NationalNo,
                 this.FirstName, this.SecondName, this.ThirdName, this.LastName
                , this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email,
                 this.NationalityCountryID,
                 this.ImagePath);

            return (this.PersonID != -1);
        }
        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID, this.NationalNo,
                 this.FirstName, this.SecondName, this.ThirdName, this.LastName
                , this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email,
                 this.NationalityCountryID,
                 this.ImagePath);
        }
        public static clsPerson Find(int PersonID)
        {
            string NationalNo = "";
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            string Phone = "";
            string Address = "";
            string Email = "";
            string ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            byte Gender = 3;

            bool IsFound = clsPersonData.GetPersonInfoByID(PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName,
                    ref LastName, ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email,
                    ref NationalityCountryID, ref ImagePath);
            if (IsFound)

                return new clsPerson(PersonID, FirstName, SecondName,
            ThirdName, LastName, NationalNo, DateOfBirth
            , Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
            else
                return null;
        }
        public static clsPerson Find(string NationalNo)
        {
            int PersonID = -1;
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            string Phone = "";
            string Address = "";
            string Email = "";
            string ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            byte Gender = 3;

            bool IsFound = clsPersonData.GetPersonInfoByNationalNo(NationalNo, ref PersonID, ref FirstName, ref SecondName, ref ThirdName,
                    ref LastName, ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email,
                    ref NationalityCountryID, ref ImagePath);
            if (IsFound)
                return new clsPerson(PersonID, FirstName, SecondName,
            ThirdName, LastName, NationalNo, DateOfBirth
            , Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
            else
                return null;
        }
        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePerson();
            }
            return false;
        }
        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }
        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }
        public static bool IsPersonExist(int PersonID)
        {
            return clsPersonData.IsPersonExist(PersonID);
        }
        public static bool IsPersonExist(string NationalNo)
        {
            return clsPersonData.IsPersonExist(NationalNo);
        }
    }
}
