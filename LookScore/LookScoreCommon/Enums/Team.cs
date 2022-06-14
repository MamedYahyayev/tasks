using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LookScoreCommon.Enums
{
    [DataContract]
    public enum Team
    {
        [EnumMember]
        UNSET,

        [EnumMember]
        HOME,

        [EnumMember]
        GUEST
    }
}
