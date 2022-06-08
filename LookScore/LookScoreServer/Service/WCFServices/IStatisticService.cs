using LookScoreCommon.Enums;
using LookScoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LookScoreServer.Service.WCFServices
{
    [ServiceContract(CallbackContract = typeof(IStatisticCallbackService))]
    public interface IStatisticService
    {
        [OperationContract] 
        void JoinToChannel();

        [OperationContract]
        void ChangeStatistic(GameStatistics statistics);

        [OperationContract]
        GameStatistics FindGameStatistics(int gameId);
    }
}
