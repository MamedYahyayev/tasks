using LookScoreCommon.Enums;
using LookScoreCommon.Model;
using LookScoreServer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace LookScoreServer.Service.WCFServices
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class StatisticService : IStatisticService
    {
        private readonly StatisticRepository _statisticRepository;
        private static readonly List<IStatisticCallbackService> _statisticCallbackServiceList = new List<IStatisticCallbackService>();

        public StatisticService()
        {
            _statisticRepository = new StatisticRepository();
        }

        public void ChangeStatistic(GameStatistics statistics)
        {
            _statisticRepository.ChangeStatistic(statistics);

            Action<IStatisticCallbackService> invoke = callback => callback.NotifyStatisticsChanged(statistics);
            _statisticCallbackServiceList.ForEach(invoke);

        }

        public GameStatistics FindGameStatistics(int gameId)
        {
            return _statisticRepository.FindGameStatistics(gameId);
        }

        public void JoinToChannel()
        {
            IStatisticCallbackService registeredClient = OperationContext.Current.GetCallbackChannel<IStatisticCallbackService>();

            if (!_statisticCallbackServiceList.Contains(registeredClient))
            {
                _statisticCallbackServiceList.Add(registeredClient);
            }
        }

        public void IncreaseGoal(GameStatistics statistics)
        {
            Action<IStatisticCallbackService> invoke = callback => callback.NotifyGoalScored(statistics);
            _statisticCallbackServiceList.ForEach(invoke);
        }

        public void DecreaseGoal(GameStatistics statistics)
        {
            Action<IStatisticCallbackService> invoke = callback => callback.NotifyGoalCancelled(statistics);
            _statisticCallbackServiceList.ForEach(invoke);
        }

        public GameStatistics[] FindAllGameStatistics()
        {
            return _statisticRepository.FindAll();
        }

        public void InitializeStatistics(Game game)
        {
            _statisticRepository.InitializeGameStatistics(game);
        }
    }
}
