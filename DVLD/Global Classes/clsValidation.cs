using System.Text.RegularExpressions;

namespace DVLD.Global_Classes
{
    public class clsValidation
    {
        public static bool ValidateEmail(string emailAddress)
        {
            var pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(emailAddress, pattern);
        }
        public static bool ValidateInteger(string Number)
        {
            string pattern = @"^[0-9]+$";
            return Regex.IsMatch(Number, pattern);
        }

        public static bool ValidateFloat(string Number)
        {
            string pattern = @"^[+-]?(\d+\.\d*|\.\d+|\d+)$";
            return Regex.IsMatch(Number, pattern);
        }
        public static bool IsNumber(string Number)
        {
            string pattern = @"^[+-]?(\d+\.\d*|\.\d+|\d+)$";
            return Regex.IsMatch(Number, pattern);
        }
    }
}
