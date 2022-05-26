using System;
using System.Runtime.Serialization;

namespace LookScoreServer.Model.Entity
{
    [Serializable]
    [DataContract]
    public class Club
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ShortName { get; set; }

        [DataMember]
        public int PlayersCount { get; set; }

        public string LogoUrl { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public DateTime FormedYear { get; set; } = new DateTime(1800, 1, 1);
    }
}
