using DVLD_Business;
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

namespace DVLD_Project.Licenses.Controls
{
    public partial class ctrlDrivingLicense : UserControl
    {
        private int _LicenseID;
        private clsLicense _License;
        public int LicenseID
        {
            get { return _LicenseID; }
        }
        public clsLicense SelectedLicenseInfo
        {
            get { return _License; }
        }

        public ctrlDrivingLicense()
        {
            InitializeComponent();
        }

        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersonInfo.Gender == 0)
                pbPersonImage.Image = Resources.male;
            else
                pbPersonImage.Image = Resources.female;

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                {
                    pbPersonImage.Load(ImagePath);
                }
                else 
                {
                    MessageBox.Show("Could not find this image: " + ImagePath, "Error", MessageBoxButtons.OK);
                }
            }

        }

        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsLicense.Find(_LicenseID);

            if (_License == null)
            {
                MessageBox.Show("License Not Found With ID: " + _LicenseID,"Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }

            lblLicenseID.Text = _LicenseID.ToString();
            lblActive.Text = _License.IsActive ? "Yes" : "No";
            lblRevoked.Text = _License.IsRevoked ? "Yes" : "No";
            lblClass.Text  =  _License.LicenseClassIfo.ClassName;
            lblName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = _License.DriverInfo.PersonInfo.Gender == 0 ? "Male" : "Female";
            lblDateOfBirth.Text = _License.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();

            lblDriverID.Text = _License.DriverID.ToString();
            lblIssueDate.Text = _License.IssueDate.ToShortDateString();
            lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();
            lblIssueReason.Text = _License.IssueReason.ToString();
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes.ToString();

            _LoadPersonImage();
        }
    }
}
