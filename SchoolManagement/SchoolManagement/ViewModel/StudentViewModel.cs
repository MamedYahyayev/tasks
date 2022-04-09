using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using SchoolManagement.Model;
using SchoolManagement.Service;

namespace SchoolManagement.ViewModel
{
    public class StudentViewModel : ReactiveObject
    {
        private readonly StudentService _studentService;
        private IList<Student> _students;


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

    }
}
