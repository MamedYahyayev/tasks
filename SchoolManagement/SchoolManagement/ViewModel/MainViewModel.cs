using ReactiveUI;
using SchoolManagement.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        private object _currentView;


        public MainViewModel()
        {
            StudentViewModel = new StudentViewModel();
            TeacherViewModel = new TeacherViewModel();
            SubjectViewModel = new SubjectViewModel();

            CurrentView = StudentViewModel;
        }

        #region Public properties for View Models

        public StudentViewModel StudentViewModel { get; set; }
        public TeacherViewModel TeacherViewModel { get; set; }
        public SubjectViewModel SubjectViewModel { get; set; }

        #endregion

        public Object CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
        }

        private void CurrentViewChange(string viewName)
        {
            switch (viewName)
            {
                case "STUDENT":
                    CurrentView = StudentViewModel;
                    break;
                case "TEACHER":
                    CurrentView = TeacherViewModel;
                    break;
                case "SUBJECT":
                    CurrentView = SubjectViewModel;
                    break;
            }
        }




        private VoidReactiveCommand<string> _currentViewChangeCommand;
        public VoidReactiveCommand<string> CurrentViewChangeCommand =>
            _currentViewChangeCommand ??= VoidReactiveCommand<string>.Create(CurrentViewChange);

    }
}
