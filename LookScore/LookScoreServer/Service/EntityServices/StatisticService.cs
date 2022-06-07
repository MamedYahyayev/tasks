using LookScoreCommon.Enums;
using LookScoreCommon.Model;
using LookScoreServer.Service.FileServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LookScoreServer.Service.EntityServices
{
    public class StatisticService
    {
        public void ChangeStatistic(GameStatistics statistics)
        {
            var gameStatistics = FindGameStatistics(statistics.GameId);
            gameStatistics.HomeClub = statistics.HomeClub;
            gameStatistics.GuestClub = statistics.GuestClub;

            DataService.Instance.SetStorageModified();
        }

        public GameStatistics FindGameStatistics(int gameId)
        {
            return DataService.Instance.Storage.GameStatistics.FirstOrDefault(x => x.GameId == gameId);
        }

    }
}
