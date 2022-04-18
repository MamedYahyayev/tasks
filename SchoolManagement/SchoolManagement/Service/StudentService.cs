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
    public class StudentService : ICrudOperation<Student>
    {
        private readonly SchoolContext _context = new SchoolContext();

        public bool Delete(int id)
        {
            var student = FindStudentById(id);

            _context.Students.Remove(student);
            _context.SaveChanges();
            return true;
        }

        public IList<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student GetById(int id)
        {
            return FindStudentById(id);
        }

        public void Insert(Student student)
        {
            _context.Students.Add(student);
        }


        public void Update(Student student)
        {
            _context.Students.Update(student);
        }

        public IList<Student> Search(string keyword)
        {
            return _context.Students
                .Where(s => s.Name.EqualsIgnoreCase(keyword) &&
                            s.Surname.EqualsIgnoreCase(keyword)).ToList();
        }


        private Student FindStudentById(int studentId)
        {
            var student = _context.Students.Find(studentId);
            if (student == null) throw new ItemNotFoundException("Student with " + studentId + " not found!");
            return student;
        }
    }
}
