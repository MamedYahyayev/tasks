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
        public void ChangeStatistic(int gameId, Team team, int amount, StatisticType statisticType)
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


            switch (statisticType)
            {
                case StatisticType.GOAL:
                    ChangeGoalStatistic(gameStatistic, team, amount);
                    break;
                case StatisticType.CORNER:
                    ChangeCornerStatistic(gameStatistic, team, amount);
                    break;
                case StatisticType.TACKLE:
                    ChangeTackleStatistic(gameStatistic, team, amount);
                    break;
                case StatisticType.SHOOT:
                    ChangeShootStatistic(gameStatistic, team, amount);
                    break;
            }


            DataService.Instance.Storage.GameStatistics = gameStatisticList.ToArray();
            DataService.Instance.SetStorageModified();
        }


        #region Statistics

        private void ChangeCornerStatistic(GameStatistics gameStatistic, Team team, int amount)
        {
            if (team == Team.HOME)
            {
                gameStatistic.HomeClub.Corner += amount;
            }
            else
            {
                gameStatistic.GuestClub.Corner += amount;
            }
        }

        private void ChangeGoalStatistic(GameStatistics gameStatistic, Team team, int amount)
        {
            if (team == Team.HOME)
            {
                gameStatistic.HomeClub.Goal += amount;
            }
            else
            {
                gameStatistic.GuestClub.Goal += amount;
            }
        }

        private void ChangeTackleStatistic(GameStatistics gameStatistic, Team team, int amount)
        {
            if (team == Team.HOME)
            {
                gameStatistic.HomeClub.Tackle += amount;
            }
            else
            {
                gameStatistic.GuestClub.Tackle += amount;
            }
        }

        private void ChangeShootStatistic(GameStatistics gameStatistic, Team team, int amount)
        {
            if (team == Team.HOME)
            {
                gameStatistic.HomeClub.Shoot += amount;
            }
            else
            {
                gameStatistic.GuestClub.Shoot += amount;
            }
        }

        #endregion

    }
}
