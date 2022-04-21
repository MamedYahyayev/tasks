using SchoolManagement.Model.Entity;

namespace SchoolManagement.Service
{
    public interface IFileService
    {
        Storage Load();
        void Save(Storage storage);
    }
}
