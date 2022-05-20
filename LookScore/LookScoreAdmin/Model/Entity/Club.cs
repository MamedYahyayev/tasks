using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookScoreAdmin.Model.Entity
{
    [Serializable]
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlayersCount { get; set; }
        public string LogoUrl { get; set; }
        public string Country { get; set; }
        public DateTime FormedYear { get; set; }
    }
}
