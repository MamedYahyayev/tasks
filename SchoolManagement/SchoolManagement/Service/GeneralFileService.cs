using SchoolManagement.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service
{
    public static class GeneralFileService
    {
        public static IFileService GetFileService(FileType fileServiceType)
        {
            switch (fileServiceType)
            {
                case FileType.JSON:
                    return new JsonFileService();
                case FileType.XML:
                    return new XmlFileService();
            }

            return null;
        }
    }
}
