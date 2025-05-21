﻿using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsPerson
    {

        public enum enMode {AddNew = 0, Update =1 }

        enMode Mode = enMode.AddNew;

        public int PersonID {  get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + MiddleName + " " + LastName; }
        }
        public string NationalNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public short Gender { get; set; }
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
            this.MiddleName = "";
            this.LastName = "";
            this.DateOfBirth = new DateTime();
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";

            Mode = enMode.AddNew;
        
        }

        private clsPerson(int PersonID, string FirstName, string MiddleName, string LastName, string NationalNo, DateTime DateOfBirth, short Gender, string Address,
            string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.MiddleName = MiddleName;
            this.LastName = LastName;
            this.NationalNo = NationalNo;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.CountryInfo = clsCountry.Find(NationalityCountryID);
            this.ImagePath = ImagePath;

            Mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.FirstName, this.MiddleName, this.LastName, this.NationalNo, this.DateOfBirth, 
                this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID, this.FirstName, this.MiddleName, this.LastName, this.NationalNo, this.DateOfBirth,
                this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
        }


        public static clsPerson Find(int PersonID)
        {
            string FirstName = "", MiddleName = "", LastName = "", NationalNo = "";
            DateTime DateOfBirth = DateTime.Now;
            short Gender = 0; string Address = "",
            Phone = "", Email = ""; int NationalityCountryID = -1; string ImagePath = "";

            bool isFound = clsPersonData.GetPersonInfoByID(PersonID, ref FirstName, ref MiddleName, ref LastName, ref NationalNo, ref DateOfBirth,
                ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath);

            if (isFound)
            { 
                return new clsPerson(PersonID, FirstName, MiddleName, LastName, NationalNo, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath); 
            }

            else
                return null;
        }

        public static clsPerson Find(string NationalNo)
        {
            int PersonID = -1;
            string FirstName = "", MiddleName = "", LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            short Gender = 0; string Address = "",
            Phone = "", Email = ""; int NationalityCountryID = -1; string ImagePath = "";

            bool isFound = clsPersonData.GetPersonInfoByNationalNo(NationalNo, ref PersonID ,ref FirstName, ref MiddleName, ref LastName, ref DateOfBirth,
                ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath);

            if (isFound)
            { return new clsPerson(PersonID, FirstName, MiddleName, LastName, NationalNo, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath); }

            else
                return null;
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if(_AddNewPerson())
                    {
                        Mode = enMode.Update;
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
