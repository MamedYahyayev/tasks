using LookScoreAdmin.Model.Entity;

namespace LookScoreAdmin.Service
{
    public interface IFileService
    {
        void Save(Storage storage);

        Storage Load();
    }
}
