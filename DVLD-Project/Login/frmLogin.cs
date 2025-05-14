using DVLD_Business;
using DVLD_Project.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser User = clsUser.FindByUsernameAndPassword(txtUsername.Text.Trim(), txtPassword.Text.Trim());

            if (User != null)
            { 
                if(chkRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                }

                else
                {
                    clsGlobal.RememberUsernameAndPassword("", "");
                }

                if(!User.IsActive)
                {
                    txtUsername.Focus();
                    MessageBox.Show("Your account is not active", "Contact Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                clsGlobal.CurrentUser = User;
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();

            }
            else
            {
                txtUsername.Focus();
                MessageBox.Show("Invalid Username or Password", "Wrong Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string username = "", password = "";

            if(clsGlobal.GetStoredCredential(ref username, ref password))
            {
                txtUsername.Text = username;
                txtPassword.Text = password;
                chkRememberMe.Checked = true;
            }

            else
                chkRememberMe.Checked = false;
        }
    }
}
