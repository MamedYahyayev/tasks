using SchoolManagement.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Utility
{
    public class FileValidator
    {
        private static FileType[] SUPPORTED_FILE_TYPES = new FileType[] { FileType.JSON, FileType.XML };

        public static bool IsValidFileType(string fileType)
        {
            foreach (var type in SUPPORTED_FILE_TYPES)
            {
               var fileTypeName = type.ToString();
                if(fileType.ToUpper() == fileTypeName.ToUpper())
                {
                    return true;
                }
            }

            return false;
        }

    }
}
