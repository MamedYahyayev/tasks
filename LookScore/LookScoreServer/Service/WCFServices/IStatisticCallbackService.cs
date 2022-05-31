using LookScoreServer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LookScoreServer.Service.WCFServices
{
    public interface IStatisticCallbackService
    {
        [OperationContract(IsOneWay = true)]
        void NotifyGoalScored(GameStatistics gameStatistics);
    }
}
