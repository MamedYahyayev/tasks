using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using SchoolManagement.Command;
using SchoolManagement.Model;
using SchoolManagement.Service;

namespace SchoolManagement.ViewModel.SubViewModel
{
    public class StudentViewModel : ReactiveObject
    {
        #region Private Properties

        private readonly StudentService _studentService = new StudentService();

        #endregion

        public StudentViewModel()
        {
            _teacherViewModel = new TeacherViewModel();
            CurrentStudent = new Student();
            Students = _studentService.GetAll().ToArray();
        }

        #region Public Properties

        private Student[] _students;
        public Student[] Students
        {
            get => _students;
            set => this.RaiseAndSetIfChanged(ref _students, value);
        }

        private Student _currentStudent;
        public Student CurrentStudent
        {
            get => _currentStudent;
            set => this.RaiseAndSetIfChanged(ref _currentStudent, value);
        }

        private TeacherViewModel _teacherViewModel;
        public TeacherViewModel TeacherViewModel
        {
            get => _teacherViewModel;
            set => this.RaiseAndSetIfChanged(ref _teacherViewModel, value);
        }

        #endregion

        #region Functions

        public void DeleteStudent(int id)
        {
            _studentService.Delete(id);
            Students = _studentService.GetAll().ToArray();
        }

        public void SearchStudent(string keyword)
        {
            Students = _studentService.Search(keyword).ToArray();
        }

        #endregion

    }
}
