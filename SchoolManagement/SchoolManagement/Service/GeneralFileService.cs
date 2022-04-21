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
        public IFileService<T> GetFileService<T>(FileType fileServiceType)
        {
            switch(fileServiceType)
            {
                case FileType.JSON:
                    return new JsonFileService<T>();
                case FileType.XML:
                    return new XmlFileService<T>();
            }

            return null;
        }
    }
}
