using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LookScoreCommon.Model
{
    [Serializable]
    [DataContract]
    public class Game
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string GameTitle { get; set; }

        [DataMember]
        public int HomeClubId { get; set; }

        [DataMember]
        public int GuestClubId { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [DataMember]
        public Club HomeClub { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [DataMember]
        public Club GuestClub { get; set; }

        [DataMember]
        public DateTime GameStartDate { get; set; } = DateTime.Now;
    }
}
