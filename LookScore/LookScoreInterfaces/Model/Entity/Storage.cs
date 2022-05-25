using System;

namespace LookScoreInterfaces.Model.Entity
{
    [Serializable]
    public class Storage
    {
        public Club[] Clubs = new Club[0];
        public Game[] Games = new Game[0];
        public Player[] Players = new Player[0];
        public GameStatistics[] GameStatistics = new GameStatistics[0];

    }
}
