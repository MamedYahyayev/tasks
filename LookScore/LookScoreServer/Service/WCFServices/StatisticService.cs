using LookScoreCommon.Enums;
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

        public void ChangeStatistic(int gameId, Team team, int amount, StatisticType statisticType)
        {
            _statisticService.ChangeStatistic(gameId, team, amount, statisticType);
        }

    }
}
