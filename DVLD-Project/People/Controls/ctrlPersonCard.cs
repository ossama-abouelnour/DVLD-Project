using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DVLD_Project.Properties;

namespace DVLD_Project.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
        private clsPerson _Person;

        private int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }      

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private void _LoadPersonImage()
        {
            if (_Person.Gender == 0)
            {
                pbPersonImage.Image = Resources.male1;
            }
            else
                pbPersonImage.Image = Resources.female;

            if (_Person.ImagePath.ToString() != "")
            {
                if (File.Exists(_Person.ImagePath.ToString()))
                    pbPersonImage.ImageLocation = pbPersonImage.ImageLocation.ToString();
                else
                    MessageBox.Show("Could not find this image: " + _Person.ImagePath.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _FillPersonInfo()
        {
            llEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblName.Text = _Person.FullName;
            lblNationalNo.Text = _Person.NationalNo;
            lblGender.Text = _Person.Gender == 0 ? "Male" : "Female";
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString();
            lblCountry.Text = clsCountry.Find(_Person.NationalityCountryID).CountryName;

            _LoadPersonImage();
        }

        public void ResetPersonInfo()
        {
            llEditPersonInfo.Enabled = false;
            lblPersonID.Text = "[???]";
            lblName.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblGender.Text = "[???]";
            lblAddress.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblCountry.Text = "[???]";
        }

        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);

            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("Person with " + NationalNo.ToString() + " is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);

            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("Person with " + PersonID.ToString() + " is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }

        private void llEditPersonInfo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(_PersonID);
            frm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }
    }
}
