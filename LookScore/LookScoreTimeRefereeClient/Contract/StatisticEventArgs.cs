﻿using LookScoreCommon.Model;
using System;

namespace LookScoreTimeRefereeClient.Contract
{
    public class StatisticEventArgs : EventArgs
    {
        public GameStatistics GameStatistics { get; set; }
    }
}
