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
    // TODO: create new method for duplicate codes
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

            var teacherService = new TeacherService();

            var teachers = new List<Teacher>();
            foreach (var teacherId in student.TeacherIds)
            {
                var teacher = teacherService.GetById(teacherId);
                teachers.Add(teacher);
            }
            student.Teachers = teachers;

            return student;
        }

        public void Insert(Student student)
        {
            student.Id = Generator.GenerateId();

            var students = DataService.Instance.Storage.Students;

            var studentList = students.ToList();

            studentList.Add(student);

            var teacherIdList = new List<int>();
            foreach (var teacher in student.Teachers)
            {
                teacherIdList.Add((int)teacher.Id);
            }

            student.TeacherIds = teacherIdList.ToArray();
            student.Teachers = new List<Teacher>();

            DataService.Instance.Storage.Students = studentList.ToArray();

            DataService.Instance.SetModified();
        }


        public void Update(Student student)
        {
            var students = DataService.Instance.Storage.Students;
            var existingStudent = students.FirstOrDefault(x => x.Id == student.Id);
            if (student == null) throw new ItemNotFoundException("Student with id: " + student.Id + " not found!");

            existingStudent.Name = student.Name;
            existingStudent.Surname = student.Surname;
            existingStudent.BirthDate = student.BirthDate;
            existingStudent.RegisterDate = student.RegisterDate;
            existingStudent.Teachers = student.Teachers;

            var teacherIdList = new List<int>();
            foreach (var teacher in student.Teachers)
            {
                teacherIdList.Add((int)teacher.Id);
            }

            existingStudent.TeacherIds = teacherIdList.ToArray();
            existingStudent.Teachers = new List<Teacher>();

            DataService.Instance.Storage.Students = students.ToArray();

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
            var students = DataService.Instance.Storage.Students;

            foreach (var student in students)
            {
                var teacherIds = student.TeacherIds;

                teacherIds = teacherIds.Where(x => x != teacherId).ToArray();

                student.TeacherIds = teacherIds;
            }

            DataService.Instance.Storage.Students = students.ToArray();

            DataService.Instance.SetModified();
        }
    }
}
