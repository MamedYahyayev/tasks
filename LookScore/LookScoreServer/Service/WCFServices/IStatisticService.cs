using LookScoreCommon.Enums;
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
        void ChangeStatistic(int gameId, Team team, int amount, StatisticType statisticType);
    }
}
