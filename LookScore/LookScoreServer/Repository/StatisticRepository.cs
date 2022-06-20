using LookScoreCommon.Model;
using LookScoreServer.Service.FileServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LookScoreServer.Repository
{
    public class StatisticRepository
    {
        #region Private Properties

        private readonly GameRepository _gameRepository;

        #endregion
        
        public StatisticRepository()
        {
            _gameRepository = new GameRepository();
        }

        public void ChangeStatistic(GameStatistics statistics)
        {
            var gameStatistics = FindGameStatistics(statistics.GameId);
            gameStatistics.HomeClub = statistics.HomeClub;
            gameStatistics.GuestClub = statistics.GuestClub;

            DataService.Instance.SetStorageModified();
        }

        public void InitializeGameStatistics(Game game)
        {
            var gameStatistics = new GameStatistics()
            {
                Game = game,
                GameId = game.Id,
                HomeClub = new Statistics(),
                GuestClub = new Statistics(),
            };

            var statisticsList = new List<GameStatistics>(DataService.Instance.Storage.GameStatistics);
            statisticsList.Add(gameStatistics);

            DataService.Instance.Storage.GameStatistics = statisticsList.ToArray();
            DataService.Instance.SetStorageModified();
        }

        public GameStatistics[] FindAll()
        {
            var statisticList = new List<GameStatistics>(DataService.Instance.Storage.GameStatistics);
            foreach (var statistic in statisticList)
            {
                statistic.Game = _gameRepository.FindOne(statistic.GameId);
            }

            return statisticList.ToArray();
        }

        public GameStatistics FindGameStatistics(int gameId)
        {
            return DataService.Instance.Storage.GameStatistics.FirstOrDefault(x => x.GameId == gameId);
        }

    }
}
