using SchoolManagement.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// TODO: Seperate Files (FileHelper and FileValidator)
namespace SchoolManagement.Utility
{
    public class FileValidator
    {
        private static FileType[] SUPPORTED_FILE_TYPES = new FileType[] { FileType.JSON, FileType.XML };
        private static string DATA_FOLDER_PATH = ConfigurationManager.AppSettings.Get("dataPath");

        public static bool IsValidFileType(string fileType)
        {
            foreach (var type in SUPPORTED_FILE_TYPES)
            {
               var fileTypeName = type.ToString();
                if(fileType.ToUpper() == fileTypeName.ToUpper())
                {
                    return true;
                }
            }

            return false;
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
            Directory.CreateDirectory(path);
        }

        private static void CreateFile(string path)
        {
            File.Create(path);
        }

        private static void CheckFileExist(string entityName, FileType fileType)
        {
            var hasDataFolder = HasBaseDataFolder();
            if (!hasDataFolder) CreateFolder(DATA_FOLDER_PATH);

            var folderPath = Path.Combine(DATA_FOLDER_PATH, entityName);
            var filePath = Path.Combine(folderPath, CreateFileName(entityName, fileType.ToString()));

            if (!Directory.Exists(folderPath))
                CreateFolder(folderPath);

            if(!File.Exists(filePath))
                CreateFile(filePath);
        }

        private static string CreateFileName(string fileName, string fileExtension)
        {
            return fileName + "." + fileExtension.ToLower();
        }


    }
}
