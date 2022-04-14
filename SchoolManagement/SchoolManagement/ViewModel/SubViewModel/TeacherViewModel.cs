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
        #region Private Properties

        private readonly TeacherService _teacherService;

        #endregion

        public TeacherListViewModel()
        {
            _teacherService = new TeacherService();
            Teachers = _teacherService.GetAll().ToArray();
            CurrentTeacher = new Teacher();
        }


        #region Public Properties

        private Teacher[] _teachers;
        public Teacher[] Teachers
        {
            get => _teachers;
            set => this.RaiseAndSetIfChanged(ref _teachers, value);
        }

        private Teacher _currentTeacher;
        public Teacher CurrentTeacher
        {
            get => _currentTeacher;
            set => this.RaiseAndSetIfChanged(ref _currentTeacher, value);
        }

        #endregion


        #region Functions

        public void DeleteTeacher(int id)
        {
            _teacherService.Delete(id);
            Teachers = _teacherService.GetAll().ToArray();
        }

        public void SearchTeacher(string keyword)
        {
            Teachers = _teacherService.Search(keyword).ToArray();
        }

        #endregion
    }
}
