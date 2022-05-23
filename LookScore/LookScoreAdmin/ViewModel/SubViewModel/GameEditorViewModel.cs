using LookScoreAdmin.Model.Entity;
using LookScoreAdmin.Service.EntityServices;
using ReactiveUI;
using LookScoreAdmin.Command;

namespace LookScoreAdmin.ViewModel.SubViewModel
{
    public class GameEditorViewModel : BaseViewModel
    {
        #region Private Properties

        private readonly ClubService _clubService;
        private GameService _gameService;
        private Game _game;
        private Club[] _clubs;

        #endregion


        public GameEditorViewModel()
        {
            _clubService = new ClubService();

            Game = new Game();
            Clubs = _clubService.FindAll();
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
            _gameService = new GameService();

            _gameService.Insert(Game);

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
