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

        private readonly IStatisticService _statisticServiceChannel;

        private Game[] _games;
        private Game _selectedGame;
        private GameStatistics _currentGameStatistic;

        #endregion

        public MainViewModel()
        {
            var callback = new GameStatisticsCallback();
            callback.StatisticsChanged += OnStatisticsChanged;

            var gameCallback = new GameCallback();
            gameCallback.GameStarted += OnGameStarted;

            InstanceContext callbackLocation = new InstanceContext(callback);
            _statisticServiceChannel = new DuplexChannelFactory<IStatisticService>(callbackLocation, "StatisticService").CreateChannel();
            _statisticServiceChannel.JoinToChannel();

            InstanceContext gameCallbackInstance = new InstanceContext(gameCallback);
            DuplexChannelFactory<IGameService> channelFactory = new DuplexChannelFactory<IGameService>(gameCallbackInstance, "GameService");
            IGameService gameService = channelFactory.CreateChannel();
            gameService.JoinToChannel();

            Games = gameService.FindAllGameDetails();
        }

        #region Public Properties

        public Game[] Games
        {
            get => _games;
            set => this.RaiseAndSetIfChanged(ref _games, value);
        }

        public Game SelectedGame
        {
            get => _selectedGame;
            set => this.RaiseAndSetIfChanged(ref _selectedGame, value);
        }

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

        #endregion


        #region Functions

        private void GameChange()
        {
            if (SelectedGame != null)
            {
                CurrentGameStatistics = _statisticServiceChannel.FindGameStatistics(SelectedGame.Id);
            }
        }

        protected virtual void OnStatisticsChanged(object source, StatisticEventArgs args)
        {
            CurrentGameStatistics = args.GameStatistics;
        }

        protected virtual void OnGameStarted(object source, GameEventArgs args)
        {
            IsGameStart = true;
            this.RaisePropertyChanged(nameof(IsGameStart));
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
