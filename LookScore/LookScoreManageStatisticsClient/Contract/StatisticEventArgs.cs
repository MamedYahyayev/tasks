using LookScoreCommon.Model;
using System;

namespace LookScoreManageStatisticsClient.Contract
{
    public class StatisticEventArgs : EventArgs
    {
        public GameStatistics GameStatistics { get; set; }
    }
}
