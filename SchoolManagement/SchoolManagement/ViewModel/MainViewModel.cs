using ReactiveUI;
using SchoolManagement.Command;
using SchoolManagement.Config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        private object _currentView;
        private string _searchInput;

        public MainViewModel()
        {
            StudentViewModel = new StudentListViewModel();
            TeacherViewModel = new TeacherListViewModel();

            CurrentView = StudentViewModel;
        }


        public StudentListViewModel StudentViewModel { get; set; }
        public TeacherListViewModel TeacherViewModel { get; set; }


        public Object CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
        }

        public string SearchInput
        {
            get => _searchInput;
            set => this.RaiseAndSetIfChanged(ref _searchInput, value);
        }

        private void CurrentViewChange(string viewName)
        {
            switch (viewName)
            {
                case "STUDENT":
                    StudentViewModel = new StudentListViewModel();
                    CurrentView = StudentViewModel;
                    break;
                case "TEACHER":
                    TeacherViewModel = new TeacherListViewModel();
                    CurrentView = TeacherViewModel;
                    break;
            }
        }

        private void InsertPerson()
        {
            if (CurrentView is StudentListViewModel)
                CurrentView = new StudentOperationViewModel(null, CurrentViewChange);
            else
                CurrentView = new TeacherOperationViewModel("INSERT", CurrentViewChange);
        }

        private void UpdatePerson()
        {
            if (CurrentView is StudentListViewModel && StudentViewModel.CurrentStudent.Id != null)
                CurrentView = new StudentOperationViewModel(StudentViewModel.CurrentStudent.Id, CurrentViewChange);
        }

        private void DeletePerson()
        {
            if (CurrentView is StudentListViewModel && StudentViewModel.CurrentStudent.Id != null)
                StudentViewModel.DeleteStudent((int)StudentViewModel.CurrentStudent.Id);
        }

        private void SearchPerson()
        {
            if (CurrentView is StudentListViewModel)
            {
                StudentViewModel.SearchStudent(SearchInput);
            }
        }

        private VoidReactiveCommand<string> _currentViewChangeCommand;
        public VoidReactiveCommand<string> CurrentViewChangeCommand =>
            _currentViewChangeCommand ??= VoidReactiveCommand<string>.Create(CurrentViewChange);

        private VoidReactiveCommand _insertPersonCommand;
        public VoidReactiveCommand InsertPersonCommand =>
            _insertPersonCommand ??= VoidReactiveCommand.Create(InsertPerson);


        private VoidReactiveCommand _updatePersonCommand;
        public VoidReactiveCommand UpdatePersonCommand =>
            _updatePersonCommand ??= VoidReactiveCommand.Create(UpdatePerson);


        private VoidReactiveCommand _deletePersonCommand;
        public VoidReactiveCommand DeletePersonCommand =>
            _deletePersonCommand ??= VoidReactiveCommand.Create(DeletePerson);


        private VoidReactiveCommand _searchCommand;
        public VoidReactiveCommand SearchCommand =>
            _searchCommand ??= VoidReactiveCommand.Create(SearchPerson);

    }
}
