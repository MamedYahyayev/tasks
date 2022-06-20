using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LookScoreCommon.Model
{
    [Serializable]
    [DataContract]
    public class GameStatistics
    {
        [DataMember]
        public Statistics HomeClub { get; set; }

        [DataMember]
        public Statistics GuestClub { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        [DataMember]
        public Game Game { get; set; }

        [DataMember]
        public int GameId { get; set; }
    }
}
