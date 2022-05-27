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
            var gameStatisticList = DataService.Instance.Storage.GameStatistics.ToList();

            GameStatistics gameStatistic;
            if (gameStatisticList.Count == 0)
            {
                gameStatistic = new GameStatistics
                {
                    GameId = gameId,
                    HomeClub = new Statistics(),
                    GuestClub = new Statistics()
                };
                gameStatisticList.Add(gameStatistic);
            }
            else
            {
                gameStatistic = gameStatisticList.FirstOrDefault(x => x.GameId == gameId);
            }


            if (team == Team.HOME)
            {
                gameStatistic.HomeClub.Goal += amount;
            }
            else
            {
                gameStatistic.GuestClub.Goal += amount;
            }


            DataService.Instance.Storage.GameStatistics = gameStatisticList.ToArray();
            DataService.Instance.SetStorageModified();
        }
    }
}
