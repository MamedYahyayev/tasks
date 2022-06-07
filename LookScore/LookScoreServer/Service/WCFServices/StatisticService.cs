﻿using LookScoreCommon.Enums;
using LookScoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LookScoreServer.Service.WCFServices
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class StatisticService : IStatisticService
    {
        private readonly EntityServices.StatisticService _statisticService;
        private static readonly List<IStatisticCallbackService> _statisticCallbackServiceList = new List<IStatisticCallbackService>();

        public StatisticService()
        {
            _statisticService = new EntityServices.StatisticService();
        }

        public void ChangeStatistic(GameStatistics statistics)
        {
            _statisticService.ChangeStatistic(statistics);

            Action<IStatisticCallbackService> invoke = callback => callback.NotifyGoalScored(statistics);
            _statisticCallbackServiceList.ForEach(invoke);

        }

        public GameStatistics FindGameStatistics(int gameId)
        {
            return _statisticService.FindGameStatistics(gameId);
        }

        public void JoinToChannel()
        {
            IStatisticCallbackService registeredClient = OperationContext.Current.GetCallbackChannel<IStatisticCallbackService>();

            if (!_statisticCallbackServiceList.Contains(registeredClient))
            {
                _statisticCallbackServiceList.Add(registeredClient);
            }

        }

    }
}
