using LookScoreCommon.Model;

namespace LookScoreServer.Service.FileServices
{
    public interface IFileService
    {
        void Save(Storage storage);

        Storage Load();
    }
}
