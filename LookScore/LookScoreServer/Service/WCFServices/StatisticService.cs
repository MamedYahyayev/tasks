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

        public void ChangeGoalStatistic(int gameId, int team, int amount)
        {
            _statisticService.ChangeGoalStatistic(gameId, (Team) team, amount);
        }
    }
}
