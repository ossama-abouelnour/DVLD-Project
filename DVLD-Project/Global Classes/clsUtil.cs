using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Global_Classes
{
    public class clsUtil
    {
        public static string GenerateGUID()
        {
            Guid guid = Guid.NewGuid();

            return guid.ToString();
        }

        public static string ReplaceFileNameWithGUID(string SourceFile)
        {
            
            FileInfo fi = new FileInfo(SourceFile);   
            string extension = fi.Extension;
            return GenerateGUID() + extension;
        }
        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            if(!Directory.Exists(FolderPath))
            {
                try 
                { 
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch(Exception ex)  
                {
                    MessageBox.Show("Could not create the folder " + ex.Message, "Error");
                    return false;
                }
            }
            return true;
        }

        public static bool CopyImageToProjectImagesFolder(ref string SourceFile)
        {
            string DestinationFolder = @"D:\repos\C#\Projects\DVLD-Project";

            if(!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }

            string DestinationFile = DestinationFolder + ReplaceFileNameWithGUID(SourceFile);

            try
            {
                File.Copy(SourceFile, DestinationFile, true);
            }
            catch (IOException iox) 
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            SourceFile = DestinationFile;
            return true;
        }



    }
}
