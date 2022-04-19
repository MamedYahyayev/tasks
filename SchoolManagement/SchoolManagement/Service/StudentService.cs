using Microsoft.EntityFrameworkCore;
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
            return _context.Students.Include(s => s.Teachers).ToList();
        }

        public Student GetById(int id)
        {
            return FindStudentById(id);
        }

        public void Insert(Student student)
        {
            AssignTeachersToStudent(student);

            _context.Students.Add(student);
            _context.SaveChanges();
        }


        public void Update(Student student)
        {
            AssignTeachersToStudent(student);

            _context.Students.Update(student);
            _context.SaveChanges();
        }

        public IList<Student> Search(string keyword)
        {
            var query = from s in _context.Students
                        where s.Name.ToLower().Contains(keyword.ToLower()) ||
                               s.Surname.ToLower().Contains(keyword.ToLower())
                        select s;

            return query.ToList();
        }

        #region Helper Functions

        private Student FindStudentById(int studentId)
        {
            var student = _context.Students.Include(s => s.Teachers).Single(s => s.Id == studentId);
            if (student == null) throw new ItemNotFoundException("Student with " + studentId + " not found!");
            return student;
        }

        private void AssignTeachersToStudent(Student student)
        {
            var teachers = new List<Teacher>();
            foreach (var teacher in student.Teachers)
            {
                var findedTeacher = _context.Teachers.Find(teacher.Id);
                teachers.Add(findedTeacher);
            }

            student.Teachers = teachers;
        }

        #endregion

    }
}
