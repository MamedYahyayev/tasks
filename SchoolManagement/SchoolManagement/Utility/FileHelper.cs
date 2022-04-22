using SchoolManagement.Enum;
using System;
using System.Configuration;
using System.IO;

namespace SchoolManagement.Utility
{
    public class FileHelper
    {
        private static string DATA_FOLDER_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Data";

        public static string GetOrCreateFile(FileType fileType)
        {
            var fileName = CreateFileName("storage", fileType.ToString().ToLowerInvariant());
            var path = Path.Combine(DATA_FOLDER_PATH, fileName);

            if (!File.Exists(path))
                CreateFile(path);

            return path;
        }

        public static FileType GetDefaultFileType(string fileType)
        {
            if (fileType.ToLower() == "xml") return FileType.XML;

            return FileType.JSON;
        }

        private static void CreateFile(string path)
        {
            CreateFolder(DATA_FOLDER_PATH);

            if (!File.Exists(path))
                File.Create(path);
        }

        private static void CreateFolder(string folderPath)
        {
            if(!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }

        private static string CreateFileName(string fileName, string fileExtension)
        {
            return fileName + "." + fileExtension.ToLower();
        }

    }
}
