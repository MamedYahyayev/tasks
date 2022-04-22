using Newtonsoft.Json;
using SchoolManagement.Enum;
using SchoolManagement.Model.Entity;
using SchoolManagement.Utility;
using System.IO;

namespace SchoolManagement.Service
{
    public class JsonFileService : IFileService
    {
        public Storage Load()
        {
            var filePath = FileHelper.GetOrCreateFile(FileType.JSON);
            Storage storage;

            if (!File.Exists(filePath))
            {
                storage = new Storage();
                Save(storage);
            }
            else
            {
                if (new FileInfo(filePath).Length == 0) return new Storage();

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

            var filePath = FileHelper.GetOrCreateFile(FileType.JSON);
            var text = JsonConvert.SerializeObject(storage);
            File.WriteAllText(filePath, text);
        }
    }
}
