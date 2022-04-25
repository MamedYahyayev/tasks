using SchoolManagement.Exceptions;
using SchoolManagement.Model;
using SchoolManagement.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SchoolManagement.Service
{
    public class TeacherService : ICrudOperation<Teacher>
    {
        private readonly StudentService _studentService = new StudentService();

        public void Delete(int id)
        {
            var teachers = GetAll(false);
            var removedTeacher = GetById(id, false);
            teachers.Remove(removedTeacher);

            _studentService.DeleteTeacherFromStudent(id);

            DataService.Instance.Storage.Teachers = teachers.ToArray();

            DataService.Instance.SetModified();
        }

        public List<Teacher> GetAll(bool includeAllFields)
        {
            var teachers = DataService.Instance.Storage.Teachers.ToList();
            
            if (includeAllFields)
                teachers.ForEach(teacher => LoadAllFields(teacher));

            return teachers ?? new List<Teacher>();
        }

        public Teacher GetById(int id, bool includeAllFields)
        {

            var teacher = DataService.Instance.Storage.Teachers.FirstOrDefault(x => x.Id == id);
            if (teacher == null) throw new ItemNotFoundException("Teacher with id: " + id + " not found!");

            if (includeAllFields)
                LoadAllFields(teacher);

            return teacher;
        }

        public void Insert(Teacher teacher)
        {
            teacher.Id = GetNextId();
            DataService.Instance.Storage.Teachers = DataService.Instance.Storage.Teachers.Concat(new[] { teacher }).ToArray();
            DataService.Instance.SetModified();
        }


        public void Update(Teacher teacher)
        {
            var existingTeacher = GetById((int)teacher.Id, false);

            existingTeacher.Name = teacher.Name;
            existingTeacher.Surname = teacher.Surname;
            existingTeacher.BirthDate = teacher.BirthDate;
            existingTeacher.License = teacher.License;
            existingTeacher.Subject = teacher.Subject;
            existingTeacher.Salary = teacher.Salary;

            DataService.Instance.SetModified();
        }

        public IList<Teacher> Search(string keyword, bool includeAllFields)
        {
            var teachers = GetAll(includeAllFields).AsEnumerable()
                                              .Where(s => s.Name.EqualsIgnoreCase(keyword) ||
                                                          s.Surname.EqualsIgnoreCase(keyword))
                                              .ToList(); ;

            return teachers;
        }

        private int GetNextId()
        {
            return DataService.Instance.Storage.Teachers.Count() + 1;
        }

        #region Helper Functions

        private void LoadAllFields(Teacher teacher)
        {
            if (teacher == null)
                return;

            var teachersStudents = new List<Student>();
            
            var students = _studentService.GetAll(false);

            foreach(var student in students)
            {
                foreach (var teacherId in student.TeacherIds)
                {
                    if(teacher.Id == teacherId)
                        teachersStudents.Add(student);
                }
            }

            teacher.Students = teachersStudents;
        }

        #endregion


        
    }
}
