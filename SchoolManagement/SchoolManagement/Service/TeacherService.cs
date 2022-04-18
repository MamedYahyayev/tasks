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
        private readonly SchoolContext _context = new SchoolContext();

        public bool Delete(int id)
        {
            var teacher = FindTeacherById(id);

            _context.Teachers.Remove(teacher);
            _context.SaveChanges();
            return true;
        }

        public IList<Teacher> GetAll()
        {
            return _context.Teachers.ToList();
        }

        public Teacher GetById(int id)
        {
            return FindTeacherById(id);
        }

        public void Insert(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
        }


        public void Update(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            _context.SaveChanges();
        }

        public IList<Teacher> Search(string keyword)
        {
            var query = from s in _context.Teachers
                        where s.Name.ToLower().Contains(keyword.ToLower()) ||
                               s.Surname.ToLower().Contains(keyword.ToLower())
                        select s;
            return query.ToList();
        }

        public int FindStudentCount(int id)
        {
            var teacher = FindTeacherById(id);
            //Console.WriteLine(teacher.Students.Count);
            return 0 /*teacher.Students.Count*/;
        }


        private Teacher FindTeacherById(int teacherId)
        {
            var teacher = _context.Teachers.Find(teacherId);
            if (teacher == null) throw new ItemNotFoundException("Teacher with " + teacherId + " not found!");
            return teacher;
        }

    }
}
