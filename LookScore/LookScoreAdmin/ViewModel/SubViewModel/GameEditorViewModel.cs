using ReactiveUI;
using LookScoreAdmin.Command;
using LookScoreServer.Service.EntityServices;
using LookScoreServer.Model.Entity;
using System.ServiceModel;
using LookScoreServer.Service.WCFServices;

namespace LookScoreAdmin.ViewModel.SubViewModel
{
    public class GameEditorViewModel : BaseViewModel
    {
        #region Private Properties

        private Game _game;
        private Club[] _clubs;
        private readonly ChannelFactory<IClubService> _clubChannelFactory = new ChannelFactory<IClubService>("ClubService");
        private readonly ChannelFactory<IGameService> _gameChannelFactory = new ChannelFactory<IGameService>("GameService");

        #endregion


        public GameEditorViewModel()
        {
            Clubs = _clubChannelFactory.CreateChannel().FindAllClubs();

            Game = new Game();
        }

        #region Public Properties

        public Game Game
        {
            get => _game;
            set => this.RaiseAndSetIfChanged(ref _game, value);
        }

        public Club[] Clubs
        {
            get => _clubs;
            set => this.RaiseAndSetIfChanged(ref _clubs, value);
        }

        #endregion


        #region Functions

        private void SaveGame()
        {
            _gameChannelFactory.CreateChannel().InsertGame(Game);

            BackToPreviousView();
        }

        private void BackToPreviousView()
        {
            MainViewModel.Instance.SetCurrentViewModel(new GameViewModel());
        }

        #endregion


        #region Commands

        private readonly VoidReactiveCommand _saveCommand;
        public VoidReactiveCommand SaveCommand =>
            _saveCommand ?? VoidReactiveCommand.Create(SaveGame);


        private readonly VoidReactiveCommand _cancelCommand;
        public VoidReactiveCommand CancelCommand =>
            _cancelCommand ?? VoidReactiveCommand.Create(BackToPreviousView);

        #endregion

    }
}
