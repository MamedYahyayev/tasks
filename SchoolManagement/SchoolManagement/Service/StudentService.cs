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
        private readonly IFileService _fileService;

        public StudentService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void Delete(int id)
        {
            var students = GetAll();

            var newStudents = students.Where(student => student.Id != id).ToList();

            string json = JsonConvert.SerializeObject(newStudents);

            _fileService.AppendData(EntityType.STUDENT, json);
        }

        public IList<Student> GetAll()
        {
            var json = _fileService.GetData(EntityType.STUDENT);

            var students = JsonConvert.DeserializeObject<List<Student>>(json);

            return students ?? new List<Student>();
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
            var existingJsonData = _fileService.GetData(EntityType.STUDENT);

            var students = JsonConvert.DeserializeObject<List<Student>>(existingJsonData);
            if (students == null) students = new List<Student>();

            student.Id = GenerateRandomId();
            students.Add(student);
            string json = JsonConvert.SerializeObject(students);

            _fileService.AppendData(EntityType.STUDENT, json);
        }


        public void Update(Student student)
        {
            var existingJsonData = _fileService.GetData(EntityType.STUDENT);

            var students = JsonConvert.DeserializeObject<List<Student>>(existingJsonData);
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
            string json = JsonConvert.SerializeObject(students);

            _fileService.AppendData(EntityType.STUDENT, json);
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
