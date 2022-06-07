using LookScoreCommon.Enums;
using LookScoreCommon.Exceptions;
using System;
using System.IO;


namespace LookScoreCommon.Util
{
    public class FileHelper
    {
        private static string STORAGE_FILE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Data\";

        private FileHelper()
        {

        }

        public static string GetOrCreateFile(FileType fileType)
        {
            var path = STORAGE_FILE_PATH + "LookScoreStorage." + fileType.ToString().ToLower();
            if (File.Exists(path))
            {
                return path;
            }

            File.Create(path);

            return path;
        }

        public static FileType GetFileType(string type)
        {
            if (type == null)
                throw new FileTypeNotConfiguredException();

            switch (type.ToLower())
            {
                case "xml":
                    return FileType.XML;
                
                case "json":
                    return FileType.JSON;
                
                default:
                    throw new UnsupportedFileTypeException();
            }
        }
    }
}
