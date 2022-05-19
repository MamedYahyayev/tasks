using LookScoreAdmin.Model.Enums;
using System;

namespace LookScoreAdmin.Model
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Height { get; set; }
        public int TshirtNo { get; set; }
        public string ImageUrl { get; set; }
        public double PlayerValue { get; set; }
        public Position Position { get; set; }
    }
}
