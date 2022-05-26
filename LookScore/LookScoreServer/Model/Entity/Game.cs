using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LookScoreServer.Model.Entity
{
    [Serializable]
    [DataContract]
    public class Game
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string GameTitle { get; set; }

        public int HomeClubId { get; set; }
        public int GuestClubId { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [DataMember]
        public Club HomeClub { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [DataMember]
        public Club GuestClub { get; set; }

        public Referee[] Referees { get; set; }

        [DataMember]
        public DateTime GameStartDate { get; set; } = DateTime.Now;
    }
}
