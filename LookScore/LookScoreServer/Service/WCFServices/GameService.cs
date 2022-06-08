using LookScoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace LookScoreServer.Service.WCFServices
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class GameService : IGameService
    {
        private static readonly List<IGameCallbackService> _callbackList = new List<IGameCallbackService>();
        private readonly EntityServices.GameService _gameService;

        public GameService()
        {
            _gameService = new EntityServices.GameService();
        }

        public Game[] FindAllGameDetails()
        {
            return _gameService.FindAllDetails();
        }

        public Game[] FindAllGames()
        {
            return _gameService.FindAll();
        }

        public string[] GetAllGamesTitle()
        {
            return _gameService.FindAll().Select(g => g.GameTitle).ToArray();
        }

        public void InsertGame(Game game)
        {
            _gameService.Insert(game);
        }

        public void JoinToChannel()
        {
            IGameCallbackService registeredClient = OperationContext.Current.GetCallbackChannel<IGameCallbackService>();

            if (!_callbackList.Contains(registeredClient))
            {
                _callbackList.Add(registeredClient);
            }
        }

        public void StartGame(Game game)
        {
            // notify all client that game start
            Action<IGameCallbackService> invoke = callback => callback.NotifyGameStarted(game);
            _callbackList.ForEach(invoke);
        
        }
    }
}
