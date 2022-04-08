using ReactiveUI;
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


        public StudentViewModel StudentViewModel { get; set; }
        public TeacherViewModel TeacherViewModel { get; set; }
        public SubjectViewModel SubjectViewModel { get; set; }

        public Object CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value); 
        }

        public MainViewModel()
        {
            StudentViewModel = new StudentViewModel();
            TeacherViewModel = new TeacherViewModel();
            SubjectViewModel = new SubjectViewModel();

            CurrentView = StudentViewModel;
        }

    }
}
