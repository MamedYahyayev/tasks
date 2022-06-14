using LookScoreCommon.Model;
using System.ServiceModel;

namespace LookScoreServer.Service.WCFServices
{
    public interface IStatisticCallbackService
    {
        [OperationContract(IsOneWay = true)]
        void NotifyGoalScored(GameStatistics gameStatistics);
    }
}
