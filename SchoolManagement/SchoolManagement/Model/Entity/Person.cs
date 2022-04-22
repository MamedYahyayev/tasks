using System;

namespace SchoolManagement.Model
{
    [Serializable]
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; } = DateTime.Now;
    }
}
