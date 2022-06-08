using LookScoreCommon.Model;
using System;

namespace LookScoreTimeRefereeClient.Contract
{
    public class GameEventArgs : EventArgs
    {
        public Game Game { get; set; }
    }
}
