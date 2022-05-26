using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookScoreServer.Model.Entity
{
    [Serializable]
    public class GameStatistics
    {
        public Statistics HomeClub { get; set; } 
        public Statistics GuestClub { get; set; }
        public Game Game { get; set; }
    }
}
