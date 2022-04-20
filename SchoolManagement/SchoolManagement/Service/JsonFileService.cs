using Newtonsoft.Json;
using SchoolManagement.Enum;
using SchoolManagement.Model;
using SchoolManagement.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service
{
    public class JsonFileService : IFileService
    {
        public void AppendData(EntityType entity, string json)
        {
            var filePath = FileValidator.GetOrCreateFile(entity, FileType.JSON);
            File.WriteAllTextAsync(filePath, json);
        }

        public void DeleteData()
        {
            throw new NotImplementedException();
        }

        public string GetData(EntityType entity)
        {
            var filePath = FileValidator.GetOrCreateFile(entity, FileType.JSON);
            string json = "";

            using (StreamReader r = new StreamReader(filePath))
            {
                json = r.ReadToEnd();
            }

            return json;
        }

        public void SearchData()
        {
            throw new NotImplementedException();
        }

        public void UpdateData()
        {
            throw new NotImplementedException();
        }
    }
}
