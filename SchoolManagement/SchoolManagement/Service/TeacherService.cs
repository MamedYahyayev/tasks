using SchoolManagement.Config;
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
    public class TeacherService : ICrudOperation<Teacher>
    {
        private readonly StudentService _studentService = new StudentService();

        public void Delete(int id)
        {
            var teachers = GetAll();
            var removedTeacher = GetById(id);
            teachers.Remove(removedTeacher);

            _studentService.DeleteTeacherFromStudent(id);

            DataService.Instance.Storage.Teachers = teachers.ToArray();

            DataService.Instance.SetModified();
        }

        public List<Teacher> GetAll()
        {
            var teachers = DataService.Instance.Storage.Teachers.ToList();
            return teachers ?? new List<Teacher>();
        }

        public Teacher GetById(int id)
        {

            var teacher = DataService.Instance.Storage.Teachers.FirstOrDefault(x => x.Id == id);
            if (teacher == null) throw new ItemNotFoundException("Teacher with id: " + id + " not found!");

            return teacher;
        }

        public void Insert(Teacher teacher)
        {
            teacher.Id = Generator.GenerateId();

            var teachers = DataService.Instance.Storage.Teachers;

            var teacherList = teachers.ToList();

            teacherList.Add(teacher);

            DataService.Instance.Storage.Teachers = teacherList.ToArray();

            DataService.Instance.SetModified();
        }


        public void Update(Teacher teacher)
        {
            var existingTeacher = GetById((int)teacher.Id);

            existingTeacher.Name = teacher.Name;
            existingTeacher.Surname = teacher.Surname;
            existingTeacher.BirthDate = teacher.BirthDate;
            existingTeacher.License = teacher.License;
            existingTeacher.Subject = teacher.Subject;
            existingTeacher.Salary = teacher.Salary;

            DataService.Instance.SetModified();
        }

        public IList<Teacher> Search(string keyword)
        {
            var teachers = GetAll().AsEnumerable()
                                              .Where(s => s.Name.EqualsIgnoreCase(keyword) ||
                                                          s.Surname.EqualsIgnoreCase(keyword))
                                              .ToList(); ;



            return teachers;
        }
    }
}
