namespace DVLD_Project
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.applicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drivingLicenseServicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newDrivingLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internationalLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renewDrivingLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lostOrReplacementLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reinstateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retakeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localDrivingLicenceApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internationalDrivingLicenceApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.revokeALicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageRevokedLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.revokeLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reinstateRevokedLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageApplicationTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageTestTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.peopleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.driversToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentUserInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Open Sans", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationsToolStripMenuItem,
            this.peopleToolStripMenuItem,
            this.driversToolStripMenuItem,
            this.usersToolStripMenuItem,
            this.accountSettingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 48);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::DVLD_Project.Properties.Resources.RoyalLogo2;
            this.pictureBox1.Location = new System.Drawing.Point(0, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 402);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // applicationsToolStripMenuItem
            // 
            this.applicationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drivingLicenseServicesToolStripMenuItem,
            this.manageApplicationsToolStripMenuItem,
            this.revokeALicenseToolStripMenuItem,
            this.manageApplicationTypesToolStripMenuItem,
            this.manageTestTypesToolStripMenuItem});
            this.applicationsToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.sign_form;
            this.applicationsToolStripMenuItem.Name = "applicationsToolStripMenuItem";
            this.applicationsToolStripMenuItem.Size = new System.Drawing.Size(192, 44);
            this.applicationsToolStripMenuItem.Text = "Applications";
            // 
            // drivingLicenseServicesToolStripMenuItem
            // 
            this.drivingLicenseServicesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDrivingLicenseToolStripMenuItem,
            this.renewDrivingLicenceToolStripMenuItem,
            this.lostOrReplacementLicenceToolStripMenuItem,
            this.reinstateToolStripMenuItem,
            this.retakeTestToolStripMenuItem});
            this.drivingLicenseServicesToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.driving_license;
            this.drivingLicenseServicesToolStripMenuItem.Name = "drivingLicenseServicesToolStripMenuItem";
            this.drivingLicenseServicesToolStripMenuItem.Size = new System.Drawing.Size(382, 46);
            this.drivingLicenseServicesToolStripMenuItem.Text = "Driving Licence Services";
            // 
            // newDrivingLicenseToolStripMenuItem
            // 
            this.newDrivingLicenseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localLicenceToolStripMenuItem,
            this.internationalLToolStripMenuItem});
            this.newDrivingLicenseToolStripMenuItem.Name = "newDrivingLicenseToolStripMenuItem";
            this.newDrivingLicenseToolStripMenuItem.Size = new System.Drawing.Size(482, 32);
            this.newDrivingLicenseToolStripMenuItem.Text = "New Driving Licence";
            // 
            // localLicenceToolStripMenuItem
            // 
            this.localLicenceToolStripMenuItem.Name = "localLicenceToolStripMenuItem";
            this.localLicenceToolStripMenuItem.Size = new System.Drawing.Size(307, 32);
            this.localLicenceToolStripMenuItem.Text = "Local Licence";
            // 
            // internationalLToolStripMenuItem
            // 
            this.internationalLToolStripMenuItem.Name = "internationalLToolStripMenuItem";
            this.internationalLToolStripMenuItem.Size = new System.Drawing.Size(307, 32);
            this.internationalLToolStripMenuItem.Text = "International Licence";
            // 
            // renewDrivingLicenceToolStripMenuItem
            // 
            this.renewDrivingLicenceToolStripMenuItem.Name = "renewDrivingLicenceToolStripMenuItem";
            this.renewDrivingLicenceToolStripMenuItem.Size = new System.Drawing.Size(482, 32);
            this.renewDrivingLicenceToolStripMenuItem.Text = "Renew Driving Licence";
            // 
            // lostOrReplacementLicenceToolStripMenuItem
            // 
            this.lostOrReplacementLicenceToolStripMenuItem.Name = "lostOrReplacementLicenceToolStripMenuItem";
            this.lostOrReplacementLicenceToolStripMenuItem.Size = new System.Drawing.Size(482, 32);
            this.lostOrReplacementLicenceToolStripMenuItem.Text = "Lost or Replacement Licence";
            // 
            // reinstateToolStripMenuItem
            // 
            this.reinstateToolStripMenuItem.Name = "reinstateToolStripMenuItem";
            this.reinstateToolStripMenuItem.Size = new System.Drawing.Size(482, 32);
            this.reinstateToolStripMenuItem.Text = "Reinstate Revoked/Suspended Licence";
            // 
            // retakeTestToolStripMenuItem
            // 
            this.retakeTestToolStripMenuItem.Name = "retakeTestToolStripMenuItem";
            this.retakeTestToolStripMenuItem.Size = new System.Drawing.Size(482, 32);
            this.retakeTestToolStripMenuItem.Text = "Retake Test";
            // 
            // manageApplicationsToolStripMenuItem
            // 
            this.manageApplicationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localDrivingLicenceApplicationToolStripMenuItem,
            this.internationalDrivingLicenceApplicationToolStripMenuItem});
            this.manageApplicationsToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.paper;
            this.manageApplicationsToolStripMenuItem.Name = "manageApplicationsToolStripMenuItem";
            this.manageApplicationsToolStripMenuItem.Size = new System.Drawing.Size(382, 46);
            this.manageApplicationsToolStripMenuItem.Text = "Manage Applications";
            // 
            // localDrivingLicenceApplicationToolStripMenuItem
            // 
            this.localDrivingLicenceApplicationToolStripMenuItem.Name = "localDrivingLicenceApplicationToolStripMenuItem";
            this.localDrivingLicenceApplicationToolStripMenuItem.Size = new System.Drawing.Size(511, 32);
            this.localDrivingLicenceApplicationToolStripMenuItem.Text = "Local Driving Licence Application";
            // 
            // internationalDrivingLicenceApplicationToolStripMenuItem
            // 
            this.internationalDrivingLicenceApplicationToolStripMenuItem.Name = "internationalDrivingLicenceApplicationToolStripMenuItem";
            this.internationalDrivingLicenceApplicationToolStripMenuItem.Size = new System.Drawing.Size(511, 32);
            this.internationalDrivingLicenceApplicationToolStripMenuItem.Text = "International Driving Licence Application";
            // 
            // revokeALicenseToolStripMenuItem
            // 
            this.revokeALicenseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageRevokedLicenceToolStripMenuItem,
            this.revokeLicenceToolStripMenuItem,
            this.reinstateRevokedLicenceToolStripMenuItem});
            this.revokeALicenseToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.disqualified;
            this.revokeALicenseToolStripMenuItem.Name = "revokeALicenseToolStripMenuItem";
            this.revokeALicenseToolStripMenuItem.Size = new System.Drawing.Size(382, 46);
            this.revokeALicenseToolStripMenuItem.Text = "Revoke/Suspend a Licence";
            // 
            // manageRevokedLicenceToolStripMenuItem
            // 
            this.manageRevokedLicenceToolStripMenuItem.Name = "manageRevokedLicenceToolStripMenuItem";
            this.manageRevokedLicenceToolStripMenuItem.Size = new System.Drawing.Size(360, 32);
            this.manageRevokedLicenceToolStripMenuItem.Text = "Manage Revoked Licence";
            // 
            // revokeLicenceToolStripMenuItem
            // 
            this.revokeLicenceToolStripMenuItem.Name = "revokeLicenceToolStripMenuItem";
            this.revokeLicenceToolStripMenuItem.Size = new System.Drawing.Size(360, 32);
            this.revokeLicenceToolStripMenuItem.Text = "Revoke Licence";
            // 
            // reinstateRevokedLicenceToolStripMenuItem
            // 
            this.reinstateRevokedLicenceToolStripMenuItem.Name = "reinstateRevokedLicenceToolStripMenuItem";
            this.reinstateRevokedLicenceToolStripMenuItem.Size = new System.Drawing.Size(360, 32);
            this.reinstateRevokedLicenceToolStripMenuItem.Text = "Reinstate Revoked Licence";
            // 
            // manageApplicationTypesToolStripMenuItem
            // 
            this.manageApplicationTypesToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.document_adjustment;
            this.manageApplicationTypesToolStripMenuItem.Name = "manageApplicationTypesToolStripMenuItem";
            this.manageApplicationTypesToolStripMenuItem.Size = new System.Drawing.Size(382, 46);
            this.manageApplicationTypesToolStripMenuItem.Text = "Manage Application Types";
            // 
            // manageTestTypesToolStripMenuItem
            // 
            this.manageTestTypesToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.test;
            this.manageTestTypesToolStripMenuItem.Name = "manageTestTypesToolStripMenuItem";
            this.manageTestTypesToolStripMenuItem.Size = new System.Drawing.Size(382, 46);
            this.manageTestTypesToolStripMenuItem.Text = "Manage Test Types";
            // 
            // peopleToolStripMenuItem
            // 
            this.peopleToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.people;
            this.peopleToolStripMenuItem.Name = "peopleToolStripMenuItem";
            this.peopleToolStripMenuItem.Size = new System.Drawing.Size(133, 44);
            this.peopleToolStripMenuItem.Text = "People";
            this.peopleToolStripMenuItem.Click += new System.EventHandler(this.peopleToolStripMenuItem_Click);
            // 
            // driversToolStripMenuItem
            // 
            this.driversToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.drivers;
            this.driversToolStripMenuItem.Name = "driversToolStripMenuItem";
            this.driversToolStripMenuItem.Size = new System.Drawing.Size(140, 44);
            this.driversToolStripMenuItem.Text = "Drivers";
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.ancestors;
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(122, 44);
            this.usersToolStripMenuItem.Text = "Users";
            // 
            // accountSettingsToolStripMenuItem
            // 
            this.accountSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentUserInfoToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.signOutToolStripMenuItem});
            this.accountSettingsToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.user_profile;
            this.accountSettingsToolStripMenuItem.Name = "accountSettingsToolStripMenuItem";
            this.accountSettingsToolStripMenuItem.Size = new System.Drawing.Size(239, 44);
            this.accountSettingsToolStripMenuItem.Text = "Account Settings";
            // 
            // currentUserInfoToolStripMenuItem
            // 
            this.currentUserInfoToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.account_setting;
            this.currentUserInfoToolStripMenuItem.Name = "currentUserInfoToolStripMenuItem";
            this.currentUserInfoToolStripMenuItem.Size = new System.Drawing.Size(291, 46);
            this.currentUserInfoToolStripMenuItem.Text = "Current User Info";
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.password;
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(291, 46);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            // 
            // signOutToolStripMenuItem
            // 
            this.signOutToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.logout;
            this.signOutToolStripMenuItem.Name = "signOutToolStripMenuItem";
            this.signOutToolStripMenuItem.Size = new System.Drawing.Size(291, 46);
            this.signOutToolStripMenuItem.Text = "Sign Out";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DVLD";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem applicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drivingLicenseServicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newDrivingLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renewDrivingLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageApplicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem revokeALicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageApplicationTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageTestTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lostOrReplacementLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reinstateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem retakeTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem peopleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem driversToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currentUserInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internationalLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localDrivingLicenceApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internationalDrivingLicenceApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageRevokedLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem revokeLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reinstateRevokedLicenceToolStripMenuItem;
    }
}

