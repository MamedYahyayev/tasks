using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using SchoolManagement.Command;
using SchoolManagement.Model;
using SchoolManagement.Service;

namespace SchoolManagement.ViewModel
{
    public class StudentViewModel : ReactiveObject
    {
        private readonly StudentService _studentService = new StudentService();
        private readonly TeacherService _teacherService = new TeacherService();

        private IList<Student> _students;
        private IList<Teacher> _teachers;
        private Student _currentStudent;
        private Teacher _currentTeacher;


        public StudentViewModel()
        {
            Students = _studentService.GetAll();
            Teachers = _teacherService.GetAll();
            CurrentStudent = new Student();
            CurrentTeacher = new Teacher();
        }

        public IList<Student> Students
        {
            get => _students;
            set => this.RaiseAndSetIfChanged(ref _students, value);
        }

        public Student CurrentStudent
        {
            get => _currentStudent;
            set => this.RaiseAndSetIfChanged(ref _currentStudent, value);
        }

        public IList<Teacher> Teachers
        {
            get => _teachers;
            set => this.RaiseAndSetIfChanged(ref _teachers, value);
        }

        public Teacher CurrentTeacher
        {
            get => _currentTeacher;
            set => this.RaiseAndSetIfChanged(ref _currentTeacher, value);
        }

        private void ClearForm() => CurrentStudent = new Student();

        private void DeleteStudent()
        {
            if(CurrentStudent.Id != null)
            {
                var isDeleted = _studentService.Delete((int) CurrentStudent.Id);
                if (isDeleted) Students = _studentService.GetAll();
            }
        }

        private void AddStudent()
        {
            if(CurrentStudent.Id == null)
            {
                if(ValidateInsertedData(CurrentStudent))
                {
                    if (CurrentTeacher.Id != null)
                        CurrentStudent.Teacher = CurrentTeacher;
                    
                    var isAdded = _studentService.Insert(CurrentStudent);
                    if (isAdded) Students = _studentService.GetAll();
                }
            }else
            {
                Console.WriteLine(CurrentStudent.Name + " " + CurrentStudent.Surname);
            }


        }

        private bool ValidateInsertedData(Student student)
        {
            return student != null && student.Name != null && student.Surname != null;
        }

        private bool ValidateUpdatedData(Student student)
        {

            return false;
        }


        private VoidReactiveCommand _clearFormCommand;
        public VoidReactiveCommand ClearFormCommand => _clearFormCommand ??= VoidReactiveCommand.Create(ClearForm);

        private VoidReactiveCommand _deleteStudentCommand;
        public VoidReactiveCommand DeleteStudentCommand => _deleteStudentCommand ??= VoidReactiveCommand.Create(DeleteStudent);

        private VoidReactiveCommand _addStudentCommand;
        public VoidReactiveCommand AddStudentCommand => _addStudentCommand ??= VoidReactiveCommand.Create(AddStudent);



    }
}
