using DVLD_Business;
using DVLD_Project.Global_Classes;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.People
{
    public partial class frmAddUpdatePerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonId);

        public event DataBackEventHandler DataBack;

        enum enMode { AddNew = 0, Update = 1 }

        enMode _Mode;

        private int _PersonID = -1;

        clsPerson _Person;

        enum enGender {Male = 0, Female = 1 }


        public frmAddUpdatePerson()
        {
            InitializeComponent();
            enMode _Mode = enMode.AddNew;
        }

        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _PersonID = PersonID;
        }

        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach(DataRow row in dtCountries.Rows)
            {
                cbCountries.Items.Add(row["CountryName"]);
            }
        }

        private void _ResetDefaultValues()
        {
            _FillCountriesInComboBox();

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add a New Person";
                _Person = new clsPerson();
            }

            else
            {
                lblTitle.Text = "Update Info";
            }

            if(rbMale.Checked)
                pbPersonImage.Image = Resources.male;
            else
                pbPersonImage.Image = Resources.female;

            llRemoveImage.Visible = (pbPersonImage.ImageLocation != null);

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-16);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-120);

            cbCountries.SelectedIndex = cbCountries.FindString("United Kingdom");

            txtFirstName.Focus();
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            rbMale.Checked = true;
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
        }

        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No person found with ID " + _PersonID, "Person Not Found", MessageBoxButtons.OK);
                this.Close();   
                return;
            }

            lblPersonID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtMiddleName.Text = _Person.MiddleName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            txtPhone.Text = _Person.Phone;

            if (_Person.Gender == 0)
            {
                rbMale.Checked = true;
            }
                
            else
            {
                rbFemale.Checked = true;
            }
                
            
            txtAddress.Text = _Person.Address;
            txtEmail.Text = _Person.Email;

            if(_Person.CountryInfo != null && !string.IsNullOrEmpty(_Person.CountryInfo.CountryName))
            {
                cbCountries.SelectedIndex = cbCountries.FindString(_Person.CountryInfo.CountryName);
            }

            else
            {
                cbCountries.SelectedIndex = -1;
            }


            if (_Person.ImagePath != "")
            {
                pbPersonImage.ImageLocation = _Person.ImagePath;
            }
            else
            {
                pbPersonImage.Image = rbMale.Checked ? Resources.male1 : Resources.female;
            }
            


            llRemoveImage.Visible = (_Person.ImagePath != "");
        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private bool _HandlePersonImage()
        {
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                    File.Delete(_Person.ImagePath);

                if (pbPersonImage.ImageLocation != null)
                {
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }

                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (! this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid, hover the mouse over the red icon", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_HandlePersonImage())
                return;


            int NationalityCountryID = clsCountry.Find(cbCountries.Text).CountryID;

            _Person.FirstName = txtFirstName.Text.ToString().Trim();
            _Person.LastName = txtLastName.Text.ToString().Trim();
            _Person.MiddleName = txtMiddleName.Text.ToString().Trim();
            _Person.Address = txtAddress.Text.ToString().Trim();
            _Person.Email = txtEmail.Text.ToString().Trim();
            _Person.Phone = txtPhone.Text.ToString().Trim();
            _Person.NationalNo = txtNationalNo.Text.ToString().Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;

            if (rbMale.Checked)
                _Person.Gender = (short)enGender.Male;
            else
                _Person.Gender = (short)enGender.Female;

            _Person.NationalityCountryID = NationalityCountryID;

            if (pbPersonImage.ImageLocation != null)
                _Person.ImagePath = pbPersonImage.ImageLocation;

            else
                _Person.ImagePath = "";

            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();

                _Mode = enMode.Update;
                lblTitle.Text = "Update Info";

                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _Person.PersonID);
            }

            else
                MessageBox.Show("Data Is Not Saved", "Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void rbFemale_Click(object sender, EventArgs e)
        {
            if(pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.female;
        }

        private void rbMale_Click(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.male1;
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBoxBase Temp = ((TextBoxBase)sender);

            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This Field is Required");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
                return;


            if(!clsValidation.ValidateEmail(txtEmail.Text.Trim()))
            {
                e.Cancel=true;
                errorProvider1.SetError(txtEmail, "Invalid Email Format");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }

        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This Field is Required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }


            if (txtNationalNo.Text.Trim() != _Person.NationalNo && clsPerson.IsPersonExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number is Already Used By Another Person!");
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string FilePath = openFileDialog1.FileName;
                pbPersonImage.Load(FilePath);
                llRemoveImage.Visible = true;
            }

        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            if (rbMale.Checked)
                pbPersonImage.Image = Resources.male1;
            else
                pbPersonImage.Image = Resources.female;

            llRemoveImage.Visible = false;
        }
    }
}
