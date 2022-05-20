using LookScoreAdmin.Model.Entity;
using LookScoreAdmin.Model.Enums;
using LookScoreAdmin.Util;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace LookScoreAdmin.Service.FileServices
{
    public class XmlFileService : IFileService
    {
        public Storage Load()
        {
            var filePath = FileHelper.GetOrCreateFile(FileType.XML);
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

            return (Storage)data;
        }

        public void Save(Storage storage)
        {
            if (storage == null)
                return;

            var filePath = FileHelper.GetOrCreateFile(FileType.XML);
            XmlSerializer serializer = new XmlSerializer(typeof(Storage));

            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, storage);
            }
        }
    }
}
