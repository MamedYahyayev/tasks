using LookScoreCommon.Model;
using LookScoreServer.Service.WCFServices;
using System;
using System.ServiceModel;

namespace LookScoreTimeRefereeClient.Contract
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class GameCallback : IGameCallbackService
    {
        public event EventHandler<GameEventArgs> GameStarted;

        public void NotifyGameStarted(Game game)
        {
            OnGameStarted(game);
        }

        protected virtual void OnGameStarted(Game game)
        {
            GameStarted?.Invoke(this, new GameEventArgs() { Game = game });
        }
    }
}
