using LookScoreCommon.Enums;
using LookScoreCommon.Exceptions;

namespace LookScoreServer.Service.FileServices
{
    public class FileServiceFactory
    {
        public static IFileService CreateFileService(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.JSON:
                    return new JsonFileService();
                case FileType.XML:
                    return new XmlFileService();
                default:
                    throw new UnsupportedFileTypeException("Unsupported file type!");
            }
        }

    }
}
