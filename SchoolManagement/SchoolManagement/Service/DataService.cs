using SchoolManagement.Model;
using SchoolManagement.Model.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Service
{
    public class DataService
    {
        public static DataService Instance { get; private set; }

        private IFileService _fileService;
        private Storage _storage;
        private bool _isStorageModified;
        private bool _shouldStopPersistence;

        public Storage Storage = new Storage();

        public DataService(IFileService fileService)
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Should call new DataService only once");
            }

            _fileService = fileService;

            _storage = _fileService.Load();
            if(_storage == null) _storage = new Storage();
            Storage = GetStorageClone();

            StartPersistence();

            Instance = this;
        }

        ~DataService()
        {
            StopPersistence();
        }

        public void StartPersistence()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    if (_shouldStopPersistence)
                    {
                        break;
                    }

                    if (_isStorageModified)
                    {
                        _fileService.Save(Storage);
                        _isStorageModified = false;
                    }

                    await Task.Delay(200);
                }
            });
        }

        public void StopPersistence()
        {
            _shouldStopPersistence = true;
        }

        public void SetModified()
        {
            _isStorageModified = true;
        }

        private Storage GetStorageClone()
        {
            var storage = _storage;
            if (storage == null) 
                return null;

            var clone = new Storage
            {
                Students = storage.Students.Select(t => new Student
                {
                    Id = t.Id,
                    Surname = t.Surname,
                    Name = t.Name,
                    BirthDate = t.BirthDate,
                    RegisterDate = t.RegisterDate,
                    TeacherIds = t.TeacherIds,
                }).ToArray(),
                Teachers = storage.Teachers.Select(t => new Teacher
                {
                    Id = t.Id,
                    Surname = t.Surname,
                    Name = t.Name,
                    BirthDate = t.BirthDate,
                    Subject = t.Subject,
                    License = t.License,
                    Salary = t.Salary,
                }).ToArray()
            };

            return clone;
        }

    }
}
