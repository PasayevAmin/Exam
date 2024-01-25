using System.Text.RegularExpressions;

namespace Indigo_Exam.Utilities.Extentions
{
    public static class UserValidator
    {
        public static bool CheckEmail(this string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            string pattern = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
            Regex regex=new Regex(pattern);
            return regex.IsMatch(email);

        }
    }
}
