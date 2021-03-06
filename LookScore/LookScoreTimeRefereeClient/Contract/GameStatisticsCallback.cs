using LookScoreCommon.Model;
using LookScoreServer.Service.WCFServices;
using System;
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
        public event EventHandler<StatisticEventArgs> StatisticsChanged;

        public void NotifyGoalCancelled(GameStatistics gameStatistics)
        {
        }

        public void NotifyGoalScored(GameStatistics gameStatistics)
        {
            OnStatisticsChanged(gameStatistics);
        }

        public void NotifyStatisticsChanged(GameStatistics gameStatistics)
        {
        }

        protected virtual void OnStatisticsChanged(GameStatistics statistics)
        {
            StatisticsChanged?.Invoke(this, new StatisticEventArgs() { GameStatistics = statistics });
        }
    }
}
