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
        private readonly StudentService _studentService = new StudentService();

        private IList<Student> _students;
        private Student _currentStudent = new Student();

        private TeacherListViewModel _teacherViewModel = new TeacherListViewModel();


        public StudentListViewModel()
        {
            Students = _studentService.GetAll();
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

        public TeacherListViewModel TeacherListViewModel
        {
            get => _teacherViewModel;
            set => this.RaiseAndSetIfChanged(ref _teacherViewModel, value);
        }

        public void DeleteStudent(int id)
        {
            _studentService.Delete(id);
            Students = _studentService.GetAll();
        }

        public void SearchStudent(string keyword)
        {
            Students = _studentService.Search(keyword);
        }



    }
}
