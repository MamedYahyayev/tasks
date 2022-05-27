using LookScoreCommon.Enums;
using LookScoreServer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LookScoreServer.Service.WCFServices
{
    [ServiceContract]
    public interface IStatisticService
    {
        [OperationContract]
        void ChangeStatistic(GameStatistics statistics);

        [OperationContract]
        GameStatistics FindGameStatistics(int gameId);
    }
}
