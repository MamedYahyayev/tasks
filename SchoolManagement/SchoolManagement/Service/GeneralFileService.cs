using SchoolManagement.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service
{
    public class GeneralFileService
    {
        public IFileService GetFileService(FileType fileServiceType)
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
