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

        public static string GetOrCreateFile(EntityType entityType, FileType fileType)
        {
            CheckFileExist(entityType, fileType);
            var fileName = entityType.ToString() + "." + fileType.ToString().ToLower();

            return Path.Combine(DATA_FOLDER_PATH, entityType.ToString(), fileName);
        }

        private static bool HasBaseDataFolder()
        {
            return Directory.Exists(DATA_FOLDER_PATH);
        }

        private static void CreateFolder(string path)
        {
            Directory.CreateDirectory(path);
        }

        private static void CreateFile(string path, string filename)
        {
            File.Create(Path.Combine(path, filename));
        }

        private static void CheckFileExist(EntityType entity, FileType fileType)
        {
            var hasDataFolder = HasBaseDataFolder();
            if (!hasDataFolder) CreateFolder(DATA_FOLDER_PATH);

            var entityFolderName = entity.ToString();
            var entityFolderPath = Path.Combine(DATA_FOLDER_PATH, entityFolderName);

            if (!Directory.Exists(entityFolderPath))
            {
                CreateFolder(entityFolderPath);
                CreateFile(entityFolderPath, entity.ToString() + "." + fileType.ToString().ToLower());
            }
        }


    }
}
