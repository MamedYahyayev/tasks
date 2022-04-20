using SchoolManagement.Enum;
using SchoolManagement.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SchoolManagement.Service
{
    public class XmlFileService<T> : IFileService<T>
    {

        public void AppendData(Type dataType, List<T> data)
        {
            var filePath = FileValidator.GetOrCreateFile(EntityType.STUDENT, FileType.XML);

            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, data);
            }

        }

        public List<T> GetData(Type entity)
        {
            var filePath = FileValidator.GetOrCreateFile(EntityType.STUDENT, FileType.XML);
            object data = null;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
            using (var reader = new StreamReader(filePath))
            {
                data = xmlSerializer.Deserialize(reader);
            }

            return (List<T>)data;
        }
    }
}
