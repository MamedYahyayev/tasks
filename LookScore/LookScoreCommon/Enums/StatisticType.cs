using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LookScoreCommon.Enums
{
    [DataContract]
    public enum StatisticType
    {
        [EnumMember]
        GOAL,

        [EnumMember]
        PASS,

        [EnumMember]
        TACKLE,

        [EnumMember]
        CORNER,

        [EnumMember]
        SHOOT

    }
}
