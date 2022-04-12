using ReactiveUI;
using SchoolManagement.Command;
using SchoolManagement.Config;
using SchoolManagement.Enum;
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

        public MainViewModel()
        {
            StudentListViewModel = new StudentListViewModel();
            TeacherListViewModel = new TeacherListViewModel();

            CurrentView = StudentListViewModel;
        }

        #region Public Properties

        public StudentListViewModel StudentListViewModel { get; set; }
        public TeacherListViewModel TeacherListViewModel { get; set; }

        private object _currentView;
        public Object CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
        }

        private string _searchInput;
        public string SearchInput
        {
            get => _searchInput;
            set => this.RaiseAndSetIfChanged(ref _searchInput, value);
        }

        #endregion

        #region Functions

        private void CurrentViewChange(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.STUDENT:
                    StudentListViewModel = new StudentListViewModel();
                    CurrentView = StudentListViewModel;
                    break;
                case ViewType.TEACHER:
                    TeacherListViewModel = new TeacherListViewModel();
                    CurrentView = TeacherListViewModel;
                    break;
            }
        }

        private void InsertPerson()
        {
            if (CurrentView is StudentListViewModel)
                CurrentView = new StudentOperationViewModel(null, CurrentViewChange);
            else if(CurrentView is TeacherListViewModel)
                CurrentView = new TeacherOperationViewModel("INSERT", CurrentViewChange);
        }

        private void UpdatePerson()
        {
            if (CurrentView is StudentListViewModel && StudentListViewModel.CurrentStudent.Id != null)
                CurrentView = new StudentOperationViewModel(StudentListViewModel.CurrentStudent.Id, CurrentViewChange);
        }

        private void DeletePerson()
        {
            if (CurrentView is StudentListViewModel && StudentListViewModel.CurrentStudent.Id != null)
                StudentListViewModel.DeleteStudent((int)StudentListViewModel.CurrentStudent.Id);
        }

        private void SearchPerson()
        {
            if (CurrentView is StudentListViewModel)
            {
                StudentListViewModel.SearchStudent(SearchInput);
            }
        }

        #endregion

        #region Commands

        private VoidReactiveCommand<ViewType> _currentViewChangeCommand;
        public VoidReactiveCommand<ViewType> CurrentViewChangeCommand =>
            _currentViewChangeCommand ??= VoidReactiveCommand<ViewType>.Create(CurrentViewChange);

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

        #endregion

    }
}
