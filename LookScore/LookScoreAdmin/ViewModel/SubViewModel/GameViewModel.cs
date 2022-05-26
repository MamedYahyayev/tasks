using System;
using ReactiveUI;
using LookScoreAdmin.Command;
using LookScoreServer.Model.Entity;
using System.ServiceModel;
using LookScoreServer.Service.WCFServices;

namespace LookScoreAdmin.ViewModel.SubViewModel
{
    public class GameViewModel : BaseViewModel
    {
        #region Private Properties

        private Game[] _games;
        
        #endregion

        public GameViewModel()
        {
            ChannelFactory<IGameService> channelFactory = new ChannelFactory<IGameService>("GameService");
            IGameService gameService = channelFactory.CreateChannel();

            Games = gameService.FindAllGames();
        }

        #region Public Properties

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
