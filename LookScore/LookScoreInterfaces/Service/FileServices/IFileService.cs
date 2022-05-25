using LookScoreInterfaces.Model.Entity;

namespace LookScoreInterfaces.Service.FileServices
{
    public interface IFileService
    {
        void Save(Storage storage);

        Storage Load();
    }
}
