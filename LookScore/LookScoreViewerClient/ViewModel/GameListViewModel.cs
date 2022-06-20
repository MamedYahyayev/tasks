using LookScoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReactiveUI;
using LookScoreServer.Service.WCFServices;
using LookScoreViewerClient.Contract;
using System.ServiceModel;

namespace LookScoreViewerClient.ViewModel
{
    public class GameListViewModel : BaseViewModel
    {
        #region Private Properties

        private readonly IGameService _gameService;
        private readonly IStatisticService _statisticService;

        #endregion

        public GameListViewModel()
        {
            InstanceContext gameCallbackInstance = new InstanceContext(new GameStatisticsCallback());
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

    }
}
