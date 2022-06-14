using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

using LookScoreCommon.Model;
using LookScoreServer.Repository;

namespace LookScoreServer.Service.WCFServices
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class GameService : IGameService
    {
        private static readonly List<IGameCallbackService> _callbackList = new List<IGameCallbackService>();
        private readonly GameRepository _gameRepository;

        public GameService()
        {
            _gameRepository = new GameRepository();
        }

        public Game[] FindAllGameDetails()
        {
            return _gameRepository.FindAllDetails();
        }

        public Game[] FindAllGames()
        {
            return _gameRepository.FindAll();
        }

        public string[] GetAllGamesTitle()
        {
            return _gameRepository.FindAll().Select(g => g.GameTitle).ToArray();
        }

        public void InsertGame(Game game)
        {
            _gameRepository.Insert(game);
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

        public void StopGame(Game game)
        {
            // notify all client that game stop
            Action<IGameCallbackService> invoke = callback =>  callback.NotifyGameStop(game);
            _callbackList.ForEach(invoke);
        }
    }
}
