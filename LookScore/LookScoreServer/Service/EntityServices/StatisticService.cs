using LookScoreCommon.Enums;
using LookScoreServer.Model.Entity;
using LookScoreServer.Service.FileServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LookScoreServer.Service.EntityServices
{
    public class StatisticService
    {
        public void ChangeGoalStatistic(int gameId, Team team, int amount)
        {
            var statistic = DataService.Instance.Storage.GameStatistics.First(x => x.GameId == gameId);

            if (team == Team.HOME)
            {
                statistic.HomeClub.Goal += amount;
            }
            else
            {
                statistic.GuestClub.Goal += amount;
            }

            var gameStatisticList = new List<GameStatistics>(DataService.Instance.Storage.GameStatistics);
            gameStatisticList.Add(statistic);
            DataService.Instance.Storage.GameStatistics = gameStatisticList.ToArray();
            DataService.Instance.SetStorageModified();
        }
    }
}
