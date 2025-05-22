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

namespace DVLD_Project.User
{
    public partial class frmAddUpdateUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1 }

        private enMode _Mode;

        private int _UserID = -1;

        clsUser _User;

        public frmAddUpdateUser()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _UserID = UserID;
        }

        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add a New User";
                this.Text = "Add a New User";
                _User = new clsUser();
                tpLoginInfo.Enabled = false;
                btnSave.Visible = false;
                pbSave.Visible = false;

                ctrlPersonCardWithFilter1.FilterFocus();
            }

            else
            {
                lblTitle.Text = "Update User Info";
                this.Text = "Update User";
                tpLoginInfo.Enabled = true;
                btnSave.Visible = true;
                pbSave.Visible = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages[1];
                txtUsername.Focus();
            }

            txtUsername.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
            
        }

        private void _LoadData()
        {
            _User = clsUser.FindByPersonID(_UserID);

            ctrlPersonCardWithFilter1.FilterEnabled = false;

            if (_User == null)
            {
                MessageBox.Show("User ID" + _UserID + " is not found", "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblUserID.Text = _User.UserID.ToString();
            txtUsername.Text = _User.UserName;
            txtPassword.Text= _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.IsActive;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
                _LoadData();
            
        }

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Visible = true;
                pbSave.Visible = true;
                tpLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages[1];
                txtUsername.Focus();
                return;
            }

            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsUser.IsUserExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected person is already a user, please select a different one.", "Error", MessageBoxButtons.OK);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }

                else
                {
                    btnSave.Visible = true;
                    pbSave.Visible = true;
                    tpLoginInfo.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages[1];
                    txtUsername.Focus();
                }
            }

            else
            {
                MessageBox.Show("Please select a person.", "Select a person", MessageBoxButtons.OK);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are missing", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _User.UserName = txtUsername.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.IsActive = chkIsActive.Checked;

            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                lblTitle.Text = "Update User";
                this.Text = "Update User";
                _Mode = enMode.Update;


                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
                MessageBox.Show("Data is Not Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                //e.Cancel = true;
                errorProvider1.SetError(txtUsername, "Username cannot be blank");
                return;
            }

            if(_Mode == enMode.AddNew)
            {
                if (clsUser.IsUserExist(txtUsername.Text.Trim()))
                {
                    //e.Cancel = true;
                    errorProvider1.SetError(txtUsername, "Username is already in use");
                    return;
                }

                else
                {
                    errorProvider1.SetError(txtUsername, null);

                }
            }

            else
            {
                if(_User.UserName != txtUsername.Text.Trim())
                {
                    if(clsUser.IsUserExist(txtUsername.Text.Trim()))
                    {
                        //e.Cancel = true;
                        errorProvider1.SetError(txtUsername, "Username is already in use");

                    }
                    else
                    {
                        errorProvider1.SetError(txtUsername, null);

                    }
                }
            }

        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                //e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password cannot be blank");
                return;
            }

            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                //e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Passwords confirmation must match the password");
                return;
            }

            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
        }

        private void frmAddUpdateUser_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }
    }
}
