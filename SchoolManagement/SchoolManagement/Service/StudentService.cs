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
            var students = _context.Students.AsEnumerable()
                                            .Where(s => s.Name.EqualsIgnoreCase(keyword) ||
                                                        s.Surname.EqualsIgnoreCase(keyword))
                                            .ToList();
            return students;

        }

        #region Helper Functions

        private Student FindStudentById(int studentId)
        {
            var student = _context.Students.Include(s => s.Teachers).Single(s => s.Id == studentId);
            if (student == null) throw new ItemNotFoundException("Student with id " + studentId + " not found!");
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
