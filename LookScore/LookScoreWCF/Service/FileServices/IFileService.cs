using LookScoreInterfaces.Model.Entity;

namespace LookScoreWCF.Service.FileServices
{
    public interface IFileService
    {
        void Save(Storage storage);

        Storage Load();
    }
}
