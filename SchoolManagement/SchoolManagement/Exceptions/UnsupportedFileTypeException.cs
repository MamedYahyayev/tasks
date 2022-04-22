using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Exceptions
{
    public class UnsupportedFileTypeException : Exception
    {
        public UnsupportedFileTypeException() { }

        public UnsupportedFileTypeException(string message) : base(message) { }
    }
}
