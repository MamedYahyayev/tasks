using ReactiveUI;
using LookScoreAdmin.Command;
using System.ServiceModel;
using LookScoreServer.Service.WCFServices;
using LookScoreCommon.Model;
using LookScoreViewerClient.Contract;

namespace LookScoreAdmin.ViewModel.SubViewModel
{
    public class GameEditorViewModel : BaseViewModel
    {
        #region Private Properties

        private readonly ChannelFactory<IClubService> _clubChannelFactory = new ChannelFactory<IClubService>("ClubService");
        private readonly IGameService _gameService;
        private readonly IStatisticService _statisticService;

        #endregion


        public GameEditorViewModel()
        {
            Clubs = _clubChannelFactory.CreateChannel().FindAllClubs();

            InstanceContext gameCallbackInstance = new InstanceContext(new GameCallback());
            var gameServiceChannelFactory = new DuplexChannelFactory<IGameService>(gameCallbackInstance, "GameService");
            _gameService = gameServiceChannelFactory.CreateChannel();

            InstanceContext statisticCallbackInstance = new InstanceContext(new GameStatisticsCallback());
            var statisticServiceChannelFactory = new DuplexChannelFactory<IStatisticService>(statisticCallbackInstance, "StatisticService");
            _statisticService = statisticServiceChannelFactory.CreateChannel();

            Game = new Game();
        }

        #region Public Properties

        private Game _game;
        public Game Game
        {
            get => _game;
            set => this.RaiseAndSetIfChanged(ref _game, value);
        }

        private Club[] _clubs;
        public Club[] Clubs
        {
            get => _clubs;
            set => this.RaiseAndSetIfChanged(ref _clubs, value);
        }

        #endregion


        #region Functions

        private void SaveGame()
        {
            Game game = _gameService.InsertGame(Game);
            _statisticService.InitializeStatistics(game);

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
