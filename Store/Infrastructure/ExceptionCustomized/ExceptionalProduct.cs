using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.ExceptionCustomized
{
    internal class ExceptionalProduct : Exception
    {
        public ExceptionalProduct(string message) : base(message) { }

        public ExceptionalProduct(string message, Exception innerException) : base(message, innerException) { }
    }
}
