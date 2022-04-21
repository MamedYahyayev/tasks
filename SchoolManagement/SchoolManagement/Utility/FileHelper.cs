using SchoolManagement.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Utility
{
    public class FileHelper
    {
        private static string DATA_FOLDER_PATH = ConfigurationManager.AppSettings.Get("dataPath");

        public static string GetStoragePath(FileType fileType)
        {
            var path = Path.Combine(DATA_FOLDER_PATH, "storage." + fileType.ToString().ToLowerInvariant());
            CreateFile(path);
            return path;
        }

        public static FileType GetDefaultFileType(string fileType)
        {
            if (fileType.ToLower() == "xml") return FileType.XML;

            return FileType.JSON;
        }

        public static string GetOrCreateFile(Type entity, FileType fileType)
        {
            CheckFileExist(entity.Name, fileType);
            var fileName = CreateFileName(entity.Name, fileType.ToString());

            return Path.Combine(DATA_FOLDER_PATH, entity.Name, fileName);
        }

        private static bool HasBaseDataFolder()
        {
            return Directory.Exists(DATA_FOLDER_PATH);
        }

        private static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private static void CreateFile(string path)
        {
            if (!File.Exists(path))
                File.Create(path);
        }

        private static void CheckFileExist(string entityName, FileType fileType)
        {
            var hasDataFolder = HasBaseDataFolder();
            if (!hasDataFolder) CreateFolder(DATA_FOLDER_PATH);

            var folderPath = Path.Combine(DATA_FOLDER_PATH, entityName);
            var filePath = Path.Combine(folderPath, CreateFileName(entityName, fileType.ToString()));

            CreateFolder(folderPath);
            CreateFile(filePath);
        }

        private static string CreateFileName(string fileName, string fileExtension)
        {
            return fileName + "." + fileExtension.ToLower();
        }

    }
}
