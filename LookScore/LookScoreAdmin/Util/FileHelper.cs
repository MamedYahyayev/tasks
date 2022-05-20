using LookScoreAdmin.Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookScoreAdmin.Util
{
    public class FileHelper
    {
        private static string STORAGE_FILE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Data\";

        public static string GetOrCreateFile(FileType fileType)
        {
            var path = STORAGE_FILE_PATH + "storage." + fileType.ToString().ToLower();
            if (File.Exists(path))
            {
                return path;
            }

            File.Create(path);

            return path;
        }
    }
}
