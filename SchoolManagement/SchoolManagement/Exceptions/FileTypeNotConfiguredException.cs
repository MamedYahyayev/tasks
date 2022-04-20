using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Exceptions
{
    public class FileTypeNotConfiguredException : Exception
    {
        public FileTypeNotConfiguredException() { }

        public FileTypeNotConfiguredException(string message) : base(message) { }
    }
}
