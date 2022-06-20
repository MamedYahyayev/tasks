using LookScoreCommon.Model;
using System;

namespace LookScoreViewerClient.Contract
{
    public class StatisticEventArgs : EventArgs
    {
        public GameStatistics GameStatistics { get; set; }
    }
}
