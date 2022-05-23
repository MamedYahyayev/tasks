using LookScoreAdmin.Model.Entity;
using LookScoreAdmin.Service.EntityServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using LookScoreAdmin.Command;

namespace LookScoreAdmin.ViewModel.SubViewModel
{
    public class GameViewModel : BaseViewModel
    {
        #region Private Properties

        private readonly GameService _gameService;
        private readonly ClubService _clubService;
        private Game[] _games;
        
        #endregion

        public GameViewModel()
        {
            _gameService = new GameService();
            _clubService = new ClubService();

            _games = _gameService.FindAll();
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
