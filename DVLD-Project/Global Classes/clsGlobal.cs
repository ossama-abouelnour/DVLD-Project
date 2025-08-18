using DVLD_Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DVLD_Project.Global_Classes
{
    internal static class clsGlobal
    {

        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";
            try
            {
                Registry.SetValue(keyPath, "Username", Username);
                Registry.SetValue(keyPath, "Password", Password);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";

            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                string usernameValue = Registry.GetValue(keyPath, "Username", null) as string;
                string passwordValue = Registry.GetValue(keyPath, "Password", null) as string;

                if (usernameValue != null && passwordValue != null)
                {
                    Username = usernameValue;
                    Password = passwordValue;
                    return true;
                }

                else
                { 
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }


    }
}
