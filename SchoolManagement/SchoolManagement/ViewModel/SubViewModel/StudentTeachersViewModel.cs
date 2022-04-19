using ReactiveUI;
using SchoolManagement.Model;
using SchoolManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.ViewModel.SubViewModel
{
    public class StudentTeachersViewModel : BaseViewModel  
    {
        #region Private Properties

        private readonly StudentService _studentService;

        #endregion

        public StudentTeachersViewModel()
        {
            _studentService = new StudentService();
            Students = _studentService.GetAll().Where(s => s.Teachers.Count != 0).ToArray();
        }


        #region Public Properties

        private Student[] _students;
        public Student[] Students
        {
            get => _students;
            set => this.RaiseAndSetIfChanged(ref _students, value);
        }

        #endregion

    }
}
