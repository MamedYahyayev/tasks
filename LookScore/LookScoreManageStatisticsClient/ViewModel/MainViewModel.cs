using System;
using System.ServiceModel;
using ReactiveUI;
using GalaSoft.MvvmLight.Command;

using LookScoreServer.Service.WCFServices;
using LookScoreCommon.Enums;
using LookScoreManageStatisticsClient.Contract;
using LookScoreCommon.Model;

namespace LookScoreManageStatisticsClient.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        #region Private Properties

        private readonly IGameService _gameService;
        private readonly IStatisticService _statisticService;

        #endregion

        public MainViewModel()
        {
            var gameStatisticsCallback = new GameStatisticsCallback();
            gameStatisticsCallback.StatisticsChanged += OnStatisticsChanged;

            var _statisticServiceChannelFactory = new DuplexChannelFactory<IStatisticService>(new InstanceContext(gameStatisticsCallback), "StatisticService");
            _statisticService = _statisticServiceChannelFactory.CreateChannel();
            _statisticService.JoinToChannel();

            var gameCallback = new GameCallback();
            gameCallback.GameStarted += OnGameStarted;
            gameCallback.GameStopped += OnGameStopped;

            var gameServiceChannelFactory = new DuplexChannelFactory<IGameService>(new InstanceContext(gameCallback), "GameService");
            _gameService = gameServiceChannelFactory.CreateChannel();
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

        private void IncreaseHomeTeamStatistics(StatisticType statistic)
        {
            IncreaseStatistics(statistic, Team.HOME);
        }

        private void IncreaseGuestTeamStatistics(StatisticType statistic)
        {
            IncreaseStatistics(statistic, Team.GUEST);
        }

        private void IncreaseStatistics(StatisticType statisticType, Team team)
        {
            switch (statisticType)
            {
                case StatisticType.GOAL:
                    ChangeGoalStatistic(team, 1);
                    _statisticService.IncreaseGoal(CurrentGameStatistics);
                    break;
                case StatisticType.CORNER:
                    ChangeCornerStatistic(team, 1);
                    break;
                case StatisticType.TACKLE:
                    ChangeTackleStatistic(team, 1);
                    break;
                case StatisticType.SHOOT:
                    ChangeShootStatistic(team, 1);
                    break;
                case StatisticType.PASS:
                    ChangePassStatistic(team, 1);
                    break;
            }

            this.RaisePropertyChanged(nameof(CurrentGameStatistics));

            _statisticService.ChangeStatistic(CurrentGameStatistics);
        }

        private void DecreaseHomeTeamStatistics(StatisticType statistic)
        {
            DecreaseStatistics(statistic, Team.HOME);
        }

        private void DecreaseGuestTeamStatistics(StatisticType statistic)
        {
            DecreaseStatistics(statistic, Team.GUEST);
        }

        private void DecreaseStatistics(StatisticType statisticType, Team team)
        {
            switch (statisticType)
            {
                case StatisticType.GOAL:
                    ChangeGoalStatistic(team, -1);
                    _statisticService.DecreaseGoal(CurrentGameStatistics);
                    break;
                case StatisticType.CORNER:
                    ChangeCornerStatistic(team, -1);
                    break;
                case StatisticType.TACKLE:
                    ChangeTackleStatistic(team, -1);
                    break;
                case StatisticType.SHOOT:
                    ChangeShootStatistic(team, -1);
                    break;
                case StatisticType.PASS:
                    ChangePassStatistic(team, -1);
                    break;
            }

            this.RaisePropertyChanged(nameof(CurrentGameStatistics));

            _statisticService.ChangeStatistic(CurrentGameStatistics);
        }

        private void GameChange()
        {
            if (SelectedGame != null)
            {
                CurrentGameStatistics = _statisticService.FindGameStatistics(SelectedGame.Id);
            }
        }


        #endregion

        #region Game Statistics Functions

        private void ChangeGoalStatistic(Team team, int amount)
        {
            if (team == Team.HOME)
            {
                CurrentGameStatistics.HomeClub.Goal += amount;
            }
            else
            {
                CurrentGameStatistics.GuestClub.Goal += amount;
            }

        }

        private void ChangeCornerStatistic(Team team, int amount)
        {
            if (team == Team.HOME)
            {
                CurrentGameStatistics.HomeClub.Corner += amount;
            }
            else
            {
                CurrentGameStatistics.GuestClub.Corner += amount;
            }
        }

        private void ChangeShootStatistic(Team team, int amount)
        {
            if (team == Team.HOME)
            {
                CurrentGameStatistics.HomeClub.Shoot += amount;
            }
            else
            {
                CurrentGameStatistics.GuestClub.Shoot += amount;
            }
        }

        private void ChangeTackleStatistic(Team team, int amount)
        {
            if (team == Team.HOME)
            {
                CurrentGameStatistics.HomeClub.Tackle += amount;
            }
            else
            {
                CurrentGameStatistics.GuestClub.Tackle += amount;
            }
        }

        private void ChangePassStatistic(Team team, int amount)
        {
            if (team == Team.HOME)
            {
                CurrentGameStatistics.HomeClub.Pass += amount;
            }
            else
            {
                CurrentGameStatistics.GuestClub.Pass += amount;
            }
        }

        #endregion

        #region Commands

        private RelayCommand<StatisticType> _increaseHomeTeamStatisticCommand;
        public RelayCommand<StatisticType> IncreaseHomeTeamStatisticCommand
        {
            get
            {
                return _increaseHomeTeamStatisticCommand ?? (_increaseHomeTeamStatisticCommand =
                                 new RelayCommand<StatisticType>(statisticType =>
                                 {
                                     IncreaseHomeTeamStatistics(statisticType);
                                 }));
            }
        }

        private RelayCommand<StatisticType> _increaseGuestTeamStatisticCommand;
        public RelayCommand<StatisticType> IncreaseGuestTeamStatisticCommand
        {
            get
            {
                return _increaseGuestTeamStatisticCommand ?? (_increaseGuestTeamStatisticCommand =
                                 new RelayCommand<StatisticType>(statisticType =>
                                 {
                                     IncreaseGuestTeamStatistics(statisticType);
                                 }));
            }
        }

        private RelayCommand<StatisticType> _decreaseHomeTeamStatisticCommand;
        public RelayCommand<StatisticType> DecreaseHomeTeamStatisticCommand
        {
            get
            {
                return _decreaseHomeTeamStatisticCommand ?? (_decreaseHomeTeamStatisticCommand =
                                 new RelayCommand<StatisticType>(statisticType =>
                                 {
                                     DecreaseHomeTeamStatistics(statisticType);
                                 }));
            }
        }

        private RelayCommand<StatisticType> _decreaseGuestTeamStatisticCommand;
        public RelayCommand<StatisticType> DecreaseGuestTeamStatisticCommand
        {
            get
            {
                return _decreaseGuestTeamStatisticCommand ?? (_decreaseGuestTeamStatisticCommand =
                                 new RelayCommand<StatisticType>(statisticType =>
                                 {
                                     DecreaseGuestTeamStatistics(statisticType);
                                 }));
            }
        }

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

    }
}
