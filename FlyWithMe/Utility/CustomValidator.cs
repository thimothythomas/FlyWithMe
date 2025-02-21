using System.Text.RegularExpressions;

namespace FlyWithMe.Utility
{
    public static class CustomValidator
    {
        public static bool IsValidUsername(string username)
        {
            string pattern = @"^[a-zA-Z0-9]{5,15}$";
            return Regex.IsMatch(username, pattern);
        }

        public static bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            return Regex.IsMatch(password, pattern);
        }
    }
}
