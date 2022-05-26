using LookScoreInterfaces.Exceptions;
//using LookScoreInterfaces.Model.Entity;
//using LookScoreInterfaces.Model.Enums;
//using LookScoreWCF.Service.FileServices;
using LookScoreInterfaces.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using LookScoreServer.Model.Entity;

namespace LookScoreWCF
{
    public class GameService : IGameService
    {
        //private readonly LookScoreWCF.Service.EntityServices.GameService _gameService;

        public GameService()
        {
            //_gameService = new LookScoreWCF.Service.EntityServices.GameService();
        }

        //public Game[] FindAllGames()
        //{
        //    return _gameService.FindAll();
        //}

        //public string[] GetAllGamesTitle()
        //{
        //    return _gameService.FindAll().Select(g => g.GameTitle).ToArray();
        //}
    }
}
