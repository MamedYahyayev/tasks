using LookScoreCommon.Model;
using LookScoreServer.Service.WCFServices;
using System;
using System.ServiceModel;

namespace LookScoreManageStatisticsClient.Contract
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class GameCallback : IGameCallbackService
    {
        public event EventHandler<GameEventArgs> GameStarted;
        public event EventHandler<GameEventArgs> GameStopped;

        public void NotifyGameStarted(Game game)
        {
            OnGameStarted(game);
        }

        public void NotifyGameStop(Game game)
        {
            OnGameStopped(game);
        }

        protected virtual void OnGameStarted(Game game)
        {
            GameStarted?.Invoke(this, new GameEventArgs() { Game = game });
        }

        protected virtual void OnGameStopped(Game game)
        {
            GameStopped?.Invoke(this, new GameEventArgs() { Game = game });
        }
    }
}
