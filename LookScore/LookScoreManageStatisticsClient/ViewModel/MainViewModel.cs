using System;
using System.ServiceModel;
using LookScoreServer.Service.WCFServices;
using ReactiveUI;
using GalaSoft.MvvmLight.Command;
using LookScoreCommon.Enums;
using LookScoreManageStatisticsClient.Contract;
using LookScoreCommon.Model;

namespace LookScoreManageStatisticsClient.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        #region Private Properties

        private readonly ChannelFactory<IGameService> _gameServiceChannel = new ChannelFactory<IGameService>("GameService");
        private readonly DuplexChannelFactory<IStatisticService> _statisticServiceChannelFactory;
        private Game[] _games;
        private Game _selectedGame;
        private GameStatistics _currentGameStatistic;

        #endregion

        public MainViewModel()
        {
            InstanceContext callbackLocation = new InstanceContext(new GameStatisticsCallback());
            _statisticServiceChannelFactory = new DuplexChannelFactory<IStatisticService>(callbackLocation, "StatisticService");
            _statisticServiceChannelFactory.CreateChannel().JoinToChannel();

            Games = _gameServiceChannel.CreateChannel().FindAllGameDetails();
            _gameServiceChannel.Close();
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
            }

            this.RaisePropertyChanged(nameof(CurrentGameStatistics));

            IStatisticService statisticService = _statisticServiceChannelFactory.CreateChannel();
            statisticService.ChangeStatistic(CurrentGameStatistics);
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
            }

            this.RaisePropertyChanged(nameof(CurrentGameStatistics));

            IStatisticService statisticService = _statisticServiceChannelFactory.CreateChannel();
            statisticService.ChangeStatistic(CurrentGameStatistics);
        }

        private void GameChange()
        {
            if (SelectedGame != null)
            {
                CurrentGameStatistics = _statisticServiceChannelFactory.CreateChannel().FindGameStatistics(SelectedGame.Id);
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

    }
}
