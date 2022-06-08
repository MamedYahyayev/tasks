﻿using LookScoreCommon.Model;
using System.ServiceModel;

namespace LookScoreServer.Service.WCFServices
{
    public interface IGameCallbackService
    {
        [OperationContract(IsOneWay = true)]
        void NotifyGameStarted(Game game);

        [OperationContract(IsOneWay = true)]
        void NotifyGameStop(Game game);
    }
}
