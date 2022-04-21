using Newtonsoft.Json;
using SchoolManagement.Enum;
using SchoolManagement.Model.Entity;
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
    public class XmlFileService : IFileService
    {
        public Storage Load()
        {
            var filePath = FileHelper.GetStoragePath(FileType.JSON);
            Storage storage;

            if (!File.Exists(filePath))
            {
                storage = new Storage();
                Save(storage);
            }
            else
            {
                var text = File.ReadAllText(filePath) ?? "";
                storage = JsonConvert.DeserializeObject<Storage>(text);

                if (storage == null)
                {
                    storage = new Storage();
                    Save(storage);
                }
            }

            return storage;
        }

        public void Save(Storage storage)
        {
            if (storage == null)
                return;

            var filePath = FileHelper.GetStoragePath(FileType.JSON);
            var text = JsonConvert.SerializeObject(storage);
            File.WriteAllText(text, filePath);
        }

        //public void AppendData(Type entity, List<T> data)
        //{
        //    var filePath = FileHelper.GetOrCreateFile(entity, FileType.XML);

        //    XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

        //    using (var writer = new StreamWriter(filePath))
        //    {
        //        serializer.Serialize(writer, data);
        //    }

        //}

        //public List<T> GetData(Type entity)
        //{
        //    var filePath = FileHelper.GetOrCreateFile(entity, FileType.XML);
        //    object data = null;

        //    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
            
        //    if (new FileInfo(filePath).Length != 0)
        //    {
        //        using (var reader = new StreamReader(filePath, Encoding.UTF8))
        //        {

        //            data = xmlSerializer.Deserialize(reader);
        //        }
        //    }

        //    return (List<T>)data;
        //}
    }
}
