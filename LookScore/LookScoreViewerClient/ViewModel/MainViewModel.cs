using GalaSoft.MvvmLight.Command;
using LookScoreCommon.Model;
using LookScoreServer.Service.WCFServices;
using LookScoreViewerClient.Contract;
using ReactiveUI;
using System.ServiceModel;

namespace LookScoreViewerClient.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        #region Private Properties

        private readonly IStatisticService _statisticService;

        #endregion

        public MainViewModel()
        {
            var callback = new GameStatisticsCallback();
            callback.StatisticsChanged += OnStatisticsChanged;

            var gameCallback = new GameCallback();
            gameCallback.GameStarted += OnGameStarted;
            gameCallback.GameStopped += OnGameStopped;

            InstanceContext callbackLocation = new InstanceContext(callback);
            _statisticService = new DuplexChannelFactory<IStatisticService>(callbackLocation, "StatisticService").CreateChannel();
            _statisticService.JoinToChannel();

            InstanceContext gameCallbackInstance = new InstanceContext(gameCallback);
            DuplexChannelFactory<IGameService> channelFactory = new DuplexChannelFactory<IGameService>(gameCallbackInstance, "GameService");
            IGameService gameService = channelFactory.CreateChannel();
            gameService.JoinToChannel();

            Games = gameService.FindAllGameDetails();
        }

        #region Public Properties

        private Game[] _games;
        public Game[] Games
        {
            get => _games;
            set => this.RaiseAndSetIfChanged(ref _games, value);
        }

        private Game _selectedGame;
        public Game SelectedGame
        {
            get => _selectedGame;
            set => this.RaiseAndSetIfChanged(ref _selectedGame, value);
        }

        private GameStatistics _currentGameStatistic;
        public GameStatistics CurrentGameStatistics
        {
            get => _currentGameStatistic;
            set => this.RaiseAndSetIfChanged(ref _currentGameStatistic, value);
        }

        private bool _isGameStart;
        public bool IsGameStart
        {
            get => _isGameStart;
            set => this.RaiseAndSetIfChanged(ref _isGameStart, value);
        }

        private bool _isGameStop;
        public bool IsGameStop
        {
            get => _isGameStop;
            set => this.RaiseAndSetIfChanged(ref _isGameStop, value);
        }

        #endregion


        #region Functions

        private void GameChange()
        {
            if (SelectedGame != null)
            {
                CurrentGameStatistics = _statisticService.FindGameStatistics(SelectedGame.Id);
            }
        }

        #endregion

        #region Events

        protected virtual void OnStatisticsChanged(object source, StatisticEventArgs args)
        {
            CurrentGameStatistics = args.GameStatistics;
        }

        protected virtual void OnGameStarted(object source, GameEventArgs args)
        {
            IsGameStart = true;
        }

        protected virtual void OnGameStopped(object source, GameEventArgs args)
        {
            IsGameStop = true;
        }

        #endregion


        #region Commands

        private RelayCommand _gameChangeCommand;
        public RelayCommand GameChangeComamnd
        {
            get
            {
                return _gameChangeCommand ?? (_gameChangeCommand =
                                 new RelayCommand(() => GameChange()));
            }
        }

        #endregion
    }
}
