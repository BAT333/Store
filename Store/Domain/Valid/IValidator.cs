using System.Text.RegularExpressions;

namespace Store.Domain.Valid
{
    internal class IValidator
    {
        public static bool IsValidEmail(String email)
        {
            return Regex.IsMatch(email, @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,})+$");
        }
        public static bool IsValidPhone(string number)
        {
            return Regex.IsMatch(number, @"^(\+\d{1,3})?\s*(\d{2})(\d{4,5})[-\s]?(\d{4})$"); ;
        }
    }
}
