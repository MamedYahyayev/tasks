using LookScoreCommon.Model;
using System;

namespace LookScoreViewerClient.Contract
{
    public class GameEventArgs : EventArgs
    {
        public Game Game { get; set; }
    }
}
