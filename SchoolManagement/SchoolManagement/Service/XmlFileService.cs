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
            var filePath = FileHelper.GetStoragePath(FileType.XML);
            Storage storage;
            object data = null;

            if (!File.Exists(filePath))
            {
                storage = new Storage();
                Save(storage);
            }
            else
            {
                
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Storage));

                if (new FileInfo(filePath).Length != 0)
                {
                    using (var reader = new StreamReader(filePath, Encoding.UTF8))
                    {

                        data = xmlSerializer.Deserialize(reader);
                    }
                }
            }

            return (Storage) data;
        }

        public void Save(Storage storage)
        {
            if (storage == null)
                return;

            var filePath = FileHelper.GetStoragePath(FileType.XML);
            XmlSerializer serializer = new XmlSerializer(typeof(Storage));

            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, storage);
            }
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
