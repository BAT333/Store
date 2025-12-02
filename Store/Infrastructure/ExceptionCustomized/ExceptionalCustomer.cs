using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.ExceptionCustomized
{
    internal class ExceptionalCustomer : Exception
    {

        public ExceptionalCustomer(string message) : base(message) { 

        }

        public ExceptionalCustomer(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
