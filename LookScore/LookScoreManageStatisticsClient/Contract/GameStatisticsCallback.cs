using LookScoreCommon.Model;
using LookScoreServer.Service.WCFServices;
using System;
using System.ServiceModel;
using System.Windows;

namespace LookScoreManageStatisticsClient.Contract
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class GameStatisticsCallback : IStatisticCallbackService
    {
        public event EventHandler<StatisticEventArgs> StatisticsChanged;

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
