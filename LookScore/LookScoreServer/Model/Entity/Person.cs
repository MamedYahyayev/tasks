using LookScoreServer.Model.Enums;
using System;

namespace LookScoreServer.Model.Entity
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
