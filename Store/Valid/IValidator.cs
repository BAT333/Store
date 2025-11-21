using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Valid
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
