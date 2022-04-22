using SchoolManagement.Exceptions;
using SchoolManagement.Model;
using SchoolManagement.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SchoolManagement.Service
{
    public class StudentService : ICrudOperation<Student>
    {
        public void Delete(int id)
        {
            var students = GetAll(false);
            var removedStudent = GetById(id, false);
            students.Remove(removedStudent);

            DataService.Instance.Storage.Students = students.ToArray();
            DataService.Instance.SetModified();
        }

        public List<Student> GetAll(bool includeAllFields)
        {
            var students = DataService.Instance.Storage.Students.ToList();
            if (includeAllFields)
            {
                students.ForEach(student => LoadAllFields(student));
            }
            return students ?? new List<Student>();
        }

        public Student GetById(int id, bool includeAllFields)
        {
            var student = DataService.Instance.Storage.Students.FirstOrDefault(x => x.Id == id);
            if (student == null) throw new ItemNotFoundException("Student with id: " + id + " not found!");

            var teacherService = new TeacherService();

            if (includeAllFields)
            {
                LoadAllFields(student);
            }

            return student;
        }

        private void LoadAllFields(Student student)
        {
            if (student == null)
                return;

            var teacherService = new TeacherService();
            var teachers = new List<Teacher>();
            foreach (var teacherId in student.TeacherIds)
            {
                var teacher = teacherService.GetById(teacherId, true);
                teachers.Add(teacher);
            }
            student.Teachers = teachers;
        }

        public void Insert(Student student)
        {
            student.Id = GetNextId();
            student.TeacherIds = AddTeacherIdsToStudent(student);
            DataService.Instance.Storage.Students.Concat(new[] { student }).ToArray();
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
            existingStudent.TeacherIds = AddTeacherIdsToStudent(student);

            DataService.Instance.Storage.Students = students.ToArray();
            DataService.Instance.SetModified();
        }

        public IList<Student> Search(string keyword, bool includeAllFields)
        {
            var students = GetAll(includeAllFields).AsEnumerable()
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

        public int GetNextId()
        {
            return DataService.Instance.Storage.Students.Count() + 1;
        }

        #region Helper Functions

        private int[] AddTeacherIdsToStudent(Student student)
        {
            var teacherIdList = new List<int>();
            foreach (var teacher in student.Teachers)
            {
                teacherIdList.Add((int)teacher.Id);
            }

            return teacherIdList.ToArray();
        }

        #endregion

    }
}
