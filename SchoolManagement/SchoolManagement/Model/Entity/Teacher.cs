using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Model
{
    [Serializable]
    public class Teacher : Person
    {
        public string License { get; set; }
        public Subject? Subject { get; set; }
        public double Salary { get; set; }
        public virtual List<Student> Students { get; set; } = new List<Student>();
    }
}
