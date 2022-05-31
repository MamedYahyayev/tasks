using LookScoreServer.Model.Entity;
using LookScoreServer.Service.WCFServices;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LookScoreTimeRefereeClient.Contract
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class GameStatisticsCallback : IStatisticCallbackService
    {
        public delegate void StatisticEventHandler(object source, StatisticEventArgs args);

        public event StatisticEventHandler StatisticsChanged;

        public void NotifyGoalScored(GameStatistics gameStatistics)
        {
            OnStatisticsChanged(gameStatistics);
        }

        protected virtual void OnStatisticsChanged(GameStatistics statistics)
        {
            StatisticsChanged?.Invoke(this, new StatisticEventArgs() { GameStatistics = statistics });
        }
    }
}
