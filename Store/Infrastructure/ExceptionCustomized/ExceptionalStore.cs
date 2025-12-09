using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.ExceptionCustomized
{
    internal class ExceptionalStore : Exception
    {
        public ExceptionalStore(string message):base(message) { }
        public ExceptionalStore(string message, Exception innerException):base(message,innerException) { }
    }
}
