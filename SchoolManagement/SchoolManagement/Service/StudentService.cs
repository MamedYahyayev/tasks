using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SchoolManagement.Config;
using SchoolManagement.Enum;
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
        private readonly IFileService<Student> _fileService;

        public StudentService(IFileService<Student> fileService)
        {
            _fileService = fileService;
        }

        public void Delete(int id)
        {
            var students = GetAll();

            var newStudents = students.Where(student => student.Id != id).ToList();

            _fileService.AppendData(typeof(Student), newStudents);
        }

        public List<Student> GetAll()
        {
            var students =  _fileService.GetData(typeof(Student));
            return students;
        }

        public Student GetById(int id)
        {
            var students = GetAll();
            var student = students.FirstOrDefault(student => student.Id == id);

            if (student == null) throw new ItemNotFoundException("Student with id " + id + " not found!");

            return student;
        }

        public void Insert(Student student)
        {
            var students = _fileService.GetData(typeof(Student));

            if (students == null) students = new List<Student>();

            student.Id = GenerateRandomId();
            students.Add(student);

            _fileService.AppendData(typeof(Student), students);
        }


        public void Update(Student student)
        {
            var students = _fileService.GetData(typeof(List<Student>));

            if (students == null) students = new List<Student>();

            var existingStudent = students.FirstOrDefault(s => s.Id == student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Surname = student.Surname;
                existingStudent.BirthDate = student.BirthDate;
                existingStudent.RegisterDate = student.RegisterDate;
                existingStudent.Teachers = student.Teachers;

            }

            _fileService.AppendData(typeof(Student), students);
        }

        public IList<Student> Search(string keyword)
        {
            var students = GetAll().AsEnumerable()
                                              .Where(s => s.Name.EqualsIgnoreCase(keyword) ||
                                                          s.Surname.EqualsIgnoreCase(keyword))
                                              .ToList();
            return students;
        }

        #region Helper Functions

        private int GenerateRandomId() => new Random().Next(1, 100_000_000);

        #endregion

    }
}
