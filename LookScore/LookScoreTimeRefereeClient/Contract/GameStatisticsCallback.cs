using LookScoreServer.Model.Entity;
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
        public void NotifyGoalScored(GameStatistics gameStatistics)
        {
            MessageBox.Show("Goal Scored");
        }
    }
}
