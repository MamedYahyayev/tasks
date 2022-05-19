using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookScoreAdmin.Model.Entity
{
    public class Game
    {
        public int Id { get; set; }
        public string GameTitle { get; set; }
        public Club HomeClub { get; set; }
        public Club GuestClub { get; set; }
        public Referee[] Referees { get; set; }
        public DateTime GameStartDate { get; set; }
    }
}
