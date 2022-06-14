using LookScoreCommon.Model;
using LookScoreServer.Service.FileServices;
using System.Linq;

namespace LookScoreServer.Repository
{
    public class StatisticRepository
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
