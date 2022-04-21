using SchoolManagement.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Utility
{
    public class FileHelper
    {
        public static FileType GetDefaultFileType(string fileType)
        {
            if(fileType.ToLower() == "xml") return FileType.XML;

            return FileType.JSON;
        }
    }
}
