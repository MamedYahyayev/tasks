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
        private readonly IFileService<Teacher> _fileService;
        private readonly StudentService _studentService;

        public TeacherService(IFileService<Teacher> fileService)
        {
            _studentService = new StudentService(new GeneralFileService().GetFileService<Student>(App.FILE_SERVICE));
            _fileService = fileService;
        }

        public void Delete(int id)
        {
            var teachers = GetAll();

            var newTeachers = teachers.Where(teacher => teacher.Id != id).ToList();

            _studentService.DeleteTeacherFromStudent(id);

            _fileService.AppendData(typeof(Teacher), newTeachers);
        }

        public List<Teacher> GetAll()
        {
            var teachers = _fileService.GetData(typeof(Teacher));
            return teachers ?? new List<Teacher>();
        }

        public Teacher GetById(int id)
        {
            var teachers = GetAll();
            var teacher = teachers.FirstOrDefault(teacher => teacher.Id == id);

            if (teacher == null) throw new ItemNotFoundException("Teacher with id " + id + " not found!");

            return teacher;
        }

        public void Insert(Teacher teacher)
        {
            var teachers = _fileService.GetData(typeof(Teacher));

            if (teachers == null) teachers = new List<Teacher>();

            teacher.Id = Generator.GenerateId();
            teachers.Add(teacher);

            _fileService.AppendData(typeof(Teacher), teachers);
        }


        public void Update(Teacher teacher)
        {
            var teachers = _fileService.GetData(typeof(Teacher));

            if (teachers == null) teachers = new List<Teacher>();

            var existingTeacher = teachers.FirstOrDefault(t => t.Id == teacher.Id);
            if (existingTeacher != null)
            {
                existingTeacher.Name = teacher.Name;
                existingTeacher.Surname = teacher.Surname;
                existingTeacher.BirthDate = teacher.BirthDate;
                existingTeacher.License = teacher.License;
                existingTeacher.Students = teacher.Students;
                existingTeacher.Salary = teacher.Salary;
                existingTeacher.Subject = teacher.Subject;

            }

            _fileService.AppendData(typeof(Teacher), teachers);
        }

        public IList<Teacher> Search(string keyword)
        {

            var teachers = GetAll().AsEnumerable()
                                               .Where(s => s.Name.EqualsIgnoreCase(keyword) ||
                                                           s.Surname.EqualsIgnoreCase(keyword))
                                               .ToList();
            return teachers;
        }
    }
}
