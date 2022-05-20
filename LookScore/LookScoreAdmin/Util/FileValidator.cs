using System.Linq;

namespace LookScoreAdmin.Util
{
    public class FileValidator
    {
        private static string[] SUPPORTED_FILE_TYPES = { "xml", "json" };

        public static bool IsValidFileType(string fileType)
        {
            if (fileType == null || fileType.Equals(""))
                return false;

            if (!SUPPORTED_FILE_TYPES.Contains(fileType.ToLower()))
                return false;

            return true;
        }
    }
}
