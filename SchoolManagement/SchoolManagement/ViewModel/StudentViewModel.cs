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
        private readonly StudentService _studentService;
        private IList<Student> _students;
        private Student _currentStudent;

        public StudentViewModel()
        {
            _studentService = new StudentService();
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

        private void ClearForm() => CurrentStudent = null;


        private VoidReactiveCommand _clearFormCommand;
        public VoidReactiveCommand ClearFormCommand => _clearFormCommand ??= VoidReactiveCommand.Create(ClearForm);
    }
}
