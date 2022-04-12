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

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _isPopupOpen, value);
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
            if (IsCurrentViewStudent())
                CurrentView = new StudentOperationViewModel(null, CurrentViewChange);
            else if (IsCurrentViewTeacher())
                CurrentView = new TeacherOperationViewModel(null, CurrentViewChange);
        }

        private void UpdatePerson()
        {
            if (IsCurrentViewStudent() && IsStudentSelected())
                CurrentView = new StudentOperationViewModel(StudentListViewModel.CurrentStudent.Id, CurrentViewChange);
            else if (IsCurrentViewTeacher() && IsTeacherSelected())
                CurrentView = new TeacherOperationViewModel(TeacherListViewModel.CurrentTeacher.Id, CurrentViewChange);

        }

        private void DeletePerson()
        {
            if (IsCurrentViewStudent() && IsStudentSelected())
                StudentListViewModel.DeleteStudent((int)StudentListViewModel.CurrentStudent.Id);

            else if (IsCurrentViewTeacher() && IsTeacherSelected())
                TeacherListViewModel.DeleteTeacher((int)TeacherListViewModel.CurrentTeacher.Id);

            IsPopupOpen = false;

        }

        private void SearchPerson()
        {
            if (IsCurrentViewStudent())
                StudentListViewModel.SearchStudent(SearchInput);
            else if (IsCurrentViewTeacher())
                TeacherListViewModel.SearchTeacher(SearchInput);
        }


        private void OpenPopup()
        {
            if (IsStudentSelected() || IsTeacherSelected()) IsPopupOpen = true;
        }

        private bool IsCurrentViewStudent() => CurrentView is StudentListViewModel;
        private bool IsCurrentViewTeacher() => CurrentView is TeacherListViewModel;
        private bool IsStudentSelected() => StudentListViewModel?.CurrentStudent?.Id != null;
        private bool IsTeacherSelected() => TeacherListViewModel?.CurrentTeacher?.Id != null;


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

        private VoidReactiveCommand _openPopupCommand;
        public VoidReactiveCommand OpenPopupCommand =>
            _openPopupCommand  ??= VoidReactiveCommand.Create(OpenPopup);

        private VoidReactiveCommand _closePopupCommand;
        public VoidReactiveCommand ClosePopupCommand =>
            _closePopupCommand ??= VoidReactiveCommand.Create(() => IsPopupOpen = false);

        #endregion

    }
}
