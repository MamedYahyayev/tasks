using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SchoolManagement.Model
{
    [Serializable]
    public class Student : Person
    {
        public DateTime? RegisterDate { get; set; }  = DateTime.Now;
        public int[] TeacherIds { get; set; } = new int[0];  
        public virtual List<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
