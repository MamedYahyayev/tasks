using LookScoreCommon.Model;
using System;

namespace LookScoreManageStatisticsClient.Contract
{
    public class GameEventArgs : EventArgs
    {
        public Game Game { get; set; }
    }
}
