namespace Store.Domain.Valid
{
    internal class IValidator
    {
        public static bool IsValidEmail(String email)
        {
            return true;
        }
        public static bool IsValidPhone(string number) 
        { 
            return true;
        }
    }
}
