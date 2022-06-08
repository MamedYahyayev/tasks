using LookScoreCommon.Enums;
using System;

namespace LookScoreCommon.Model
{
    [Serializable]
    public class Player : Person
    {
        public int Height { get; set; }
        public string ImageUrl { get; set; }
        public double PlayerValue { get; set; }
        public Position Position { get; set; }
    }
}
