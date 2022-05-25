using LookScoreInterfaces.Exceptions;
using LookScoreInterfaces.Model.Entity;
using LookScoreInterfaces.Model.Enums;
using LookScoreWCF.Service.FileServices;
using LookScoreInterfaces.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LookScoreWCF
{
    public class GameService : IGameService
    {
        private readonly LookScoreWCF.Service.EntityServices.GameService _gameService;

        public GameService()
        {
            var fileType = "json";
            if (string.IsNullOrEmpty(fileType)) throw new FileTypeNotConfiguredException("File Type doesn't exist in config file!");

            var isValid = FileValidator.IsValidFileType(fileType);
            if (!isValid) throw new UnsupportedFileTypeException("Invalid File Type, please use json or xml file type");

            FileType fileService = FileHelper.GetFileType(fileType);

            DataService.InitInstance(FileServiceFactory.CreateFileService(fileService));


            _gameService = new LookScoreWCF.Service.EntityServices.GameService();
        }

        public Game[] FindAllGames()
        {
            return _gameService.FindAll();
        }

        public string[] GetAllGamesTitle()
        {
            return _gameService.FindAll().Select(g => g.GameTitle).ToArray();
        }
    }
}
