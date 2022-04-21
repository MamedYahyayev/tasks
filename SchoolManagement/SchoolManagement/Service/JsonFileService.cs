﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class JsonFileService<T> : IFileService<T>
    {
        public void AppendData(Type entity, List<T> data)
        {
            object serializedData = JsonConvert.SerializeObject(data);

            var filePath = FileValidator.GetOrCreateFile(entity, FileType.JSON);
            File.WriteAllTextAsync(filePath, serializedData.ToString());

        }

        public List<T> GetData(Type entity)
        {
            var filePath = FileValidator.GetOrCreateFile(entity, FileType.JSON);
            string text = "";
            if (new FileInfo(filePath).Length != 0)
            {
                text = File.ReadAllText(filePath);
            }

            return JsonConvert.DeserializeObject<List<T>>(text);
        }
    }
}
