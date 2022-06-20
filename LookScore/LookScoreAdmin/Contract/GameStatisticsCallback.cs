using LookScoreCommon.Model;
using LookScoreServer.Service.WCFServices;
using System;
using System.ServiceModel;

namespace LookScoreViewerClient.Contract
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class GameStatisticsCallback : IStatisticCallbackService
    {
        public event EventHandler<StatisticEventArgs> StatisticsChanged;
        public event EventHandler<StatisticEventArgs> GoalScored;
        public event EventHandler<StatisticEventArgs> GoalCancelled;

        public void NotifyGoalCancelled(GameStatistics gameStatistics)
        {
            OnGoalCancelled(gameStatistics);
        }

        public void NotifyGoalScored(GameStatistics gameStatistics)
        {
            OnGoalScored(gameStatistics);
        }

        public void NotifyStatisticsChanged(GameStatistics gameStatistics)
        {
            OnStatisticsChanged(gameStatistics);
        }

        protected virtual void OnStatisticsChanged(GameStatistics statistics)
        {
            StatisticsChanged?.Invoke(this, new StatisticEventArgs() { GameStatistics = statistics });
        }

        protected virtual void OnGoalScored(GameStatistics statistics)
        {
            GoalScored?.Invoke(this, new StatisticEventArgs() { GameStatistics = statistics });
        }

        protected virtual void OnGoalCancelled(GameStatistics statistics)
        {
            GoalCancelled?.Invoke(this, new StatisticEventArgs() { GameStatistics = statistics });
        }
    }
}
