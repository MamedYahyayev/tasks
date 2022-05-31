using LookScoreServer.Model.Entity;
using LookScoreServer.Service.WCFServices;
using System.ServiceModel;
using System.Windows;

namespace LookScoreManageStatisticsClient.Contract
{
    //[CallbackBehavior(UseSynchronizationContext = false)]
    public class GameStatisticsCallback : IStatisticCallbackService
    {
        public void NotifyGoalScored(GameStatistics gameStatistics)
        {

        }
    }
}
