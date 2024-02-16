using DVLD_Business;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
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
                //gets the current project's directory
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                // Path for the file that contains the credential.
                string filePath = currentDirectory + "\\data.json";




                // Check if the file exists before attempting to read it
                if (File.Exists(filePath))
                {
                    // Create a StreamReader to read from the file
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(User));

                    // Deserialize the object back
                    using (FileStream stream = new FileStream("data.json", FileMode.Open))
                    {
                        User deserializedPerson = (User)serializer.ReadObject(stream);

                        Username = deserializedPerson.UserName;
                        Password = deserializedPerson.Password;

                    }
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

        //serialization
        public static bool RememberUserNameAndPassword(string username, string password)
        {
            try
            {
                //we selected current folder to save file in.
                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();

                string FilePath = CurrentDirectory + "\\data.json";

                if (File.Exists(FilePath))
                {
                    User user = new User { UserName = username, Password = password };

                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(User));
                    using (MemoryStream stream = new MemoryStream())
                    {
                        serializer.WriteObject(stream, user);
                        string jsonString = System.Text.Encoding.UTF8.GetString(stream.ToArray());

                        // Save the JSON string to a file (optional)
                        File.WriteAllText("data.json", jsonString);
                    }
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
