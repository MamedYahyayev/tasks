using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace LookScoreAdmin.Model.Entity
{
    [Serializable]
    public class Game
    {
        public int Id { get; set; }
        public string GameTitle { get; set; }
        public int HomeClubId { get; set; }
        public int GuestClubId { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public Club HomeClub { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public Club GuestClub { get; set; }

        public Referee[] Referees { get; set; }
        public DateTime GameStartDate { get; set; } = DateTime.Now;
    }
}
