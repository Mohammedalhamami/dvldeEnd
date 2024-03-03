using DVLD_Business;
using Microsoft.Win32;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
namespace DVLD.Global_Classes
{
    public static class clsGlobal
    {
        public static clsUser CurrentUser;

        //we add attribute Serializable to make class has interface serialize.
        [Serializable]
        public class User
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        //deserialization
        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\LogInfo";
                string valueName = "Log";

                string valueData = Registry.GetValue(keyPath, valueName, null) as string;

                if (valueData != null)
                {
                    string[] split = valueData.Split(new Char[] { '#', '/', });
                    Username = split[0];
                    Password = split[5];
                    return true;
                }
                else
                {
                    MessageBox.Show($"An error occurred");
                    return false;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }



        public static bool RememberUserNameAndPassword(string username, string password)
        {
            //Encrypting password for one-way using hasing sha-256;
            using (SHA256 hash = SHA256.Create())
            {
                byte[] hashBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                password = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
            //we selected current folder to save file in.
            //string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\LogInfo";
            string valueName = "Log";
            string valueData = string.Join("//#//", username, password);

            try
            {

                Registry.SetValue(keyPath, valueName, valueData, RegistryValueKind.String);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error: {e.Message}", "Error Registry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // string FilePath = CurrentDirectory + "\\data.jso
        }
    }
}
