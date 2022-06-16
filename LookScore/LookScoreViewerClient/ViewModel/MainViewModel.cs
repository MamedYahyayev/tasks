using GalaSoft.MvvmLight.Command;
using LookScoreCommon.Model;
using LookScoreServer.Service.WCFServices;
using LookScoreViewerClient.Contract;
using ReactiveUI;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

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
            callback.GoalScored += OnGoalScored;
            callback.GoalCancelled += OnGoalCancelled;

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

        private string _notificationTitle;
        public string NotificationTitle
        {
            get => _notificationTitle;
            set => this.RaiseAndSetIfChanged(ref _notificationTitle, value);
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _isPopupOpen, value);
        }

        private string _icon;
        public string Icon
        {
            get => _icon;
            set => this.RaiseAndSetIfChanged(ref _icon, value);
        }

        private string _iconColor;
        public string IconColor
        {
            get => _iconColor;
            set => this.RaiseAndSetIfChanged(ref _iconColor, value);
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

        protected virtual void OnGoalScored(object source, StatisticEventArgs args)
        {
            NotificationTitle = "Goal!!!";
            IsPopupOpen = true;
            Icon = "Check";
            IconColor = "Green";

            Task.Run(async () =>
            {
                await Task.Delay(5000);
                IsPopupOpen = false;
            });
        }

        protected virtual void OnGoalCancelled(object source, StatisticEventArgs args)
        {
            NotificationTitle = "Goal Cancelled...";
            IsPopupOpen = true;
            Icon = "Close";
            IconColor = "Red";

            Task.Run(async () =>
            {
                await Task.Delay(5000);
                IsPopupOpen = false;
            });
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
