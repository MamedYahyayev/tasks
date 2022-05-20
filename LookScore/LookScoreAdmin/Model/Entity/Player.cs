using LookScoreAdmin.Model.Enums;
using System;

namespace LookScoreAdmin.Model.Entity
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
