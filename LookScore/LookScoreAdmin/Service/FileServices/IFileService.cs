using LookScoreAdmin.Model.Entity;

namespace LookScoreAdmin.Service.FileServices
{
    public interface IFileService
    {
        void Save(Storage storage);

        Storage Load();
    }
}
