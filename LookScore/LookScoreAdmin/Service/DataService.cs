using LookScoreAdmin.Model.Entity;
using System;
using System.Threading.Tasks;

namespace LookScoreAdmin.Service
{
    public class DataService
    {
        private static DataService _instance;
        public static DataService Instance => _instance;

        private Storage _storage;
        private IFileService _fileService;
        private bool _shouldContinuePersistence = true;
        private bool _isStorageModified;

        public Storage Storage = new Storage();

        private DataService(IFileService fileService)
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("You cannot create multiple instance of DataService");
            }

            _fileService = fileService;

            _storage = _fileService.Load();
            if (_storage == null) _storage = new Storage();
            
            StartPersistence();

            _instance = this;
        }

        ~DataService()
        {
            StopPersistence();
        }


        private void StartPersistence()
        {
            Task.Run(async () =>
            {
                while (_shouldContinuePersistence)
                {
                    if (_isStorageModified)
                    {
                        _fileService.Save(Storage);
                        _isStorageModified = false;
                    }

                    await Task.Delay(200);
                }
            });
        }

        private void StopPersistence()
        {
            _shouldContinuePersistence = false;
        }

        public void SetStorageModified()
        {
            _isStorageModified = true;
        }


        public void InitInstance(IFileService fileService)
        {
            _ = new DataService(fileService);
        }

    }
}
