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
    public class StudentListViewModel : ReactiveObject
    {
        #region Private Properties

        private readonly StudentService _studentService = new StudentService();

        #endregion

        public StudentListViewModel()
        {
            _teacherListViewModel = new TeacherListViewModel();
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

        private TeacherListViewModel _teacherListViewModel;
        public TeacherListViewModel TeacherListViewModel
        {
            get => _teacherListViewModel;
            set => this.RaiseAndSetIfChanged(ref _teacherListViewModel, value);
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
