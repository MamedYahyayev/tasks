using LookScoreCommon.Enums;
using System;

namespace LookScoreCommon.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Nationality Nationality { get; set; }
    }
}
