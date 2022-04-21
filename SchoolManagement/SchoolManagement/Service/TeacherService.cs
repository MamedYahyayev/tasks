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
    // TODO: Create a new helper method for generating id move GenerateRandomId to a static method and call it meaningfully
    public class TeacherService : ICrudOperation<Teacher>
    {
        private readonly IFileService<Teacher> _fileService;

        public TeacherService(IFileService<Teacher> fileService)
        {
            _fileService = fileService;
        }

        public void Delete(int id)
        {
            var teachers = GetAll();

            var findedTeacher = teachers.Where(teacher => teacher.Id != id).ToList();

            _fileService.AppendData(typeof(Teacher), findedTeacher);
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

            teacher.Id = GenerateRandomId();
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


        #region Helper Functions

        private int GenerateRandomId() => new Random().Next(1, 100_000_000);

        #endregion


    }
}
