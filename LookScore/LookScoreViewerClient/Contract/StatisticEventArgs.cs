using LookScoreServer.Model.Entity;
using System;

namespace LookScoreViewerClient.Contract
{
    public class StatisticEventArgs : EventArgs
    {
        public GameStatistics GameStatistics { get; set; }
    }
}
