using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SchoolManagement.Config;
using SchoolManagement.Enum;
using SchoolManagement.Exceptions;
using SchoolManagement.Model;
using SchoolManagement.Utility;
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
        public void Delete(int id)
        {
            var students = GetAll();
            var removedStudent = GetById(id);
            students.Remove(removedStudent);

            DataService.Instance.Storage.Students = students.ToArray();

            DataService.Instance.SetModified();
        }

        public List<Student> GetAll()
        {
            var students = DataService.Instance.Storage.Students.ToList();
            return students ?? new List<Student>();
        }

        public Student GetById(int id)
        {
            var student = DataService.Instance.Storage.Students.FirstOrDefault(x => x.Id == id);
            if (student == null) throw new ItemNotFoundException("Student with id: " + id + " not found!");

            return student;
        }

        public void Insert(Student student)
        {
            student.Id = Generator.GenerateId();

            var students = DataService.Instance.Storage.Students;

            var studentList = students.ToList();

            studentList.Add(student);

            DataService.Instance.Storage.Students = studentList.ToArray();

            DataService.Instance.SetModified();
        }


        public void Update(Student student)
        {
            var existingStudent = GetById((int)student.Id);

            existingStudent.Name = student.Name;
            existingStudent.Surname = student.Surname;
            existingStudent.BirthDate = student.BirthDate;
            existingStudent.RegisterDate = student.RegisterDate;
            existingStudent.Teachers = student.Teachers;

            DataService.Instance.SetModified();
        }

        public IList<Student> Search(string keyword)
        {
            var students = GetAll().AsEnumerable()
                                              .Where(s => s.Name.EqualsIgnoreCase(keyword) ||
                                                          s.Surname.EqualsIgnoreCase(keyword))
                                              .ToList(); ;



            return students;
        }

        public void DeleteTeacherFromStudent(int teacherId)
        {
            var students = GetAll();

            foreach (var student in students)
            {
                for (var i = 0; i < student.Teachers.Count; i++)
                {
                    if (student.Teachers[i].Id == teacherId)
                        student.Teachers.RemoveAt(i);
                }
            }
        }
    }
}
