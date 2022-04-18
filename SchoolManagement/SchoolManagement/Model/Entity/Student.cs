using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Model
{
    public class Student : Person
    {
        public DateTime? RegisterDate { get; set; }  = DateTime.Now;
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
