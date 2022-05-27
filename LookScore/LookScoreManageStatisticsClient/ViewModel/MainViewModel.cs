using System;
using System.ServiceModel;
using LookScoreServer.Model.Entity;
using LookScoreServer.Service.WCFServices;
using ReactiveUI;
using GalaSoft.MvvmLight.Command;
using LookScoreCommon.Enums;

namespace LookScoreManageStatisticsClient.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        #region Private Properties

        private Game[] _games;
        private Game _selectedGame;
        private readonly ChannelFactory<IGameService> _gameServiceChannel = new ChannelFactory<IGameService>("GameService");
        private readonly ChannelFactory<IStatisticService> _statisticServiceChannel = new ChannelFactory<IStatisticService>("StatisticService");
        #endregion

        public MainViewModel()
        {
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

        private void IncreaseStatistics(StatisticType statistic, Team team)
        {
            IStatisticService statisticService = _statisticServiceChannel.CreateChannel();
            statisticService.ChangeStatistic(SelectedGame.Id, team, 1, statistic);
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

        #endregion

    }
}
