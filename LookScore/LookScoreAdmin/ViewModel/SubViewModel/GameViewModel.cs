using System;
using ReactiveUI;
using LookScoreAdmin.Command;
using System.ServiceModel;
using LookScoreServer.Service.WCFServices;
using LookScoreCommon.Model;
using LookScoreViewerClient.Contract;

namespace LookScoreAdmin.ViewModel.SubViewModel
{
    public class GameViewModel : BaseViewModel
    {
        #region Private Properties

        private readonly IGameService _gameService;

        #endregion

        public GameViewModel()
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


        #region Functions

        private void OpenNewGameView()
        {
            MainViewModel.Instance.SetCurrentViewModel(new GameEditorViewModel());
        }

        #endregion


        #region Commands

        private readonly VoidReactiveCommand _newGameViewCommand;
        public VoidReactiveCommand NewGameViewCommand =>
            _newGameViewCommand ?? VoidReactiveCommand.Create(OpenNewGameView);

        #endregion

    }
}
