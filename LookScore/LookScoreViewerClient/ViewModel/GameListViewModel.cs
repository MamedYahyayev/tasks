using LookScoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReactiveUI;
using LookScoreServer.Service.WCFServices;
using LookScoreViewerClient.Contract;
using System.ServiceModel;

namespace LookScoreViewerClient.ViewModel
{
    public class GameListViewModel : BaseViewModel
    {
        #region Private Properties

        private readonly IGameService _gameService;

        #endregion

        public GameListViewModel()
        {
            var gameCallback = new GameCallback();

            InstanceContext gameCallbackInstance = new InstanceContext(gameCallback);
            DuplexChannelFactory<IGameService> channelFactory = new DuplexChannelFactory<IGameService>(gameCallbackInstance, "GameService");
            _gameService = channelFactory.CreateChannel();
            _gameService.JoinToChannel();

            Games = _gameService.FindAllGameDetails();
        }

        #region Public Properties

        private Game[] _games;
        public Game[] Games
        {
            get => _games;
            set => this.RaiseAndSetIfChanged(ref _games, value);
        }

        #endregion

    }
}
