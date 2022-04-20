using SchoolManagement.Config;
using SchoolManagement.Exceptions;
using SchoolManagement.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service
{
    public class TeacherService : ICrudOperation<Teacher>
    {
        public void Delete(int id)
        {

        }

        public IList<Teacher> GetAll()
        {
            return new List<Teacher>();
        }

        public Teacher GetById(int id)
        {
            return null;
        }

        public void Insert(Teacher teacher)
        {
           
        }


        public void Update(Teacher teacher)
        {
            
        }

        public IList<Teacher> Search(string keyword)
        {
            
            return null;
        }

    }
}
