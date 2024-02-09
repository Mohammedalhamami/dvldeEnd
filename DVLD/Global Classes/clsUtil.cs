using System;
using System.IO;
using System.Windows.Forms;

namespace DVLD.Global_Classes
{
    internal class clsUtil
    {
        public static string GenerateGUID()
        {
            //Generate a new guid.
            Guid NewGuid = Guid.NewGuid();

            //convert guid to string.
            return NewGuid.ToString();
        }
        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            //check if the folder exist
            if (!Directory.Exists(FolderPath))
            {
                //if does not exist, then created one.
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;

                }
                catch (Exception e)
                {
                    MessageBox.Show("Error Creating Folder:" + e.Message);
                    return false;
                }
            }
            return true;
        }
        public static string ReplaceFileNameWithGUID(string sourcefile)
        {
            //FileInof: represent crud operations for files.
            FileInfo fi = new FileInfo(sourcefile);
            string extn = fi.Extension;
            return GenerateGUID() + extn;
        }
        public static bool CopyImageToProjectImagesFolder(ref string SourceFile)
        {
            //we need the folder path.
            string DestinationFolder = @"C:\DVLD-People-Images\";

            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }

            string destinationFile = DestinationFolder + ReplaceFileNameWithGUID(SourceFile);

            try
            {
                File.Copy(SourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            SourceFile = destinationFile;
            return true;
        }


    }
}
