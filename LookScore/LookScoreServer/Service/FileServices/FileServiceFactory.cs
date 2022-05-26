using LookScoreInterfaces.Exceptions;
using LookScoreServer.Model.Enums;

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
