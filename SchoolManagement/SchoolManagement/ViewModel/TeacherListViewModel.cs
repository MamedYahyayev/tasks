using ReactiveUI;
using SchoolManagement.Model;
using SchoolManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.ViewModel
{
    public class TeacherListViewModel : ReactiveObject
    {
        private readonly TeacherService _teacherService;
        private IList<Teacher> _teachers;
        private Teacher _currentTeacher;

        public TeacherListViewModel()
        {
            _teacherService = new TeacherService();
            Teachers = _teacherService.GetAll();
            CurrentTeacher = new Teacher();
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
    }
}
