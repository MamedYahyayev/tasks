using System;

namespace SchoolManagement.Model.Entity
{
    [Serializable]
    public class Storage
    {
        public Student[] Students = new Student[0];
        public Teacher[] Teachers = new Teacher[0];
    }
}
