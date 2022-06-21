using LookScoreCommon.Model;
using System;

using ReactiveUI;
using LookScoreServer.Service.WCFServices;
using LookScoreViewerClient.Contract;
using System.ServiceModel;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace LookScoreViewerClient.ViewModel
{
    public class GameListViewModel : BaseViewModel
    {
        #region Private Properties

        private readonly IStatisticService _statisticService;

        #endregion

        public GameListViewModel()
        {
            var gameStatisticsCallback = new GameStatisticsCallback();
            gameStatisticsCallback.StatisticsChanged += OnStatisticsChanged;

            var gameCallbackInstance = new InstanceContext(gameStatisticsCallback);
            var statisticChannelFactory = new DuplexChannelFactory<IStatisticService>(gameCallbackInstance, "StatisticService");
            _statisticService = statisticChannelFactory.CreateChannel();
            _statisticService.JoinToChannel();

            GameStatistics = _statisticService.FindAllGameStatistics();
        }

        #region Public Properties

        private GameStatistics[] _gameStatistics;
        public GameStatistics[] GameStatistics
        {
            get => _gameStatistics;
            set => this.RaiseAndSetIfChanged(ref _gameStatistics, value);
        }

        #endregion


        #region Functions

        private void SelectGame(int gameId)
        {
            MainViewModel.Instance.SetCurrentView(new GameViewModel(gameId));
        }

        #endregion

        #region Events

        protected virtual void OnStatisticsChanged(object source, StatisticEventArgs args)
        {
            var gameStatistic = args.GameStatistics;

            var copiedGameStatistics = new GameStatistics[GameStatistics.Length];

            Array.Copy(GameStatistics, copiedGameStatistics, _gameStatistics.Length);
            int index = Array.FindIndex(copiedGameStatistics, row => row.GameId == gameStatistic.GameId);
            copiedGameStatistics[index] = gameStatistic;

            GameStatistics = copiedGameStatistics;
        }

        #endregion


        #region Commands

        private RelayCommand<int> _selectGameCommand;
        public RelayCommand<int> SelectGameCommand
        {
            get
            {
                return _selectGameCommand ?? (_selectGameCommand =
                                    new RelayCommand<int>((gameId) => SelectGame(gameId)));
            }
        }

        #endregion
    }
}
