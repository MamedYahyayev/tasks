using LookScoreCommon.Enums;
using LookScoreServer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LookScoreServer.Service.WCFServices
{
    public class StatisticService : IStatisticService
    {
        private readonly EntityServices.StatisticService _statisticService;

        public StatisticService()
        {
            _statisticService = new EntityServices.StatisticService();
        }

        public void ChangeStatistic(GameStatistics statistics)
        {
            _statisticService.ChangeStatistic(statistics);
        }

        public GameStatistics FindGameStatistics(int gameId)
        {
            return _statisticService.FindGameStatistics(gameId);
        }
    }
}
