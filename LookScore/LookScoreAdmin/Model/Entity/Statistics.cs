using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookScoreAdmin.Model.Entity
{
    [Serializable]
    public class Statistics
    {
        public int Goal { get; set; }
        public int Corner { get; set; }
        public int Tackle { get; set; }
        public int Pass { get; set; }
        public int BallPossessionTime { get; set; }
    }
}
