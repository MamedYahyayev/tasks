using LookScoreServer.Model.Entity;

namespace LookScoreServer.Service.FileServices
{
    public interface IFileService
    {
        void Save(Storage storage);

        Storage Load();
    }
}
