using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using SchoolManagement.Command;
using SchoolManagement.Model;
using SchoolManagement.Service;

namespace SchoolManagement.ViewModel.SubViewModel
{
    public class StudentViewModel : BaseViewModel
    {
        #region Private Properties

        private readonly StudentService _studentService = new StudentService(new JsonFileService<Student>());

        #endregion

        public StudentViewModel()
        {
            _teacherViewModel = new TeacherViewModel();
            CurrentStudent = new Student();
            Students = _studentService.GetAll().ToArray();
        }

        #region Public Properties

        private Student[] _students;
        public Student[] Students
        {
            get => _students;
            set => this.RaiseAndSetIfChanged(ref _students, value);
        }

        private Student _currentStudent;
        public Student CurrentStudent
        {
            get => _currentStudent;
            set => this.RaiseAndSetIfChanged(ref _currentStudent, value);
        }

        private TeacherViewModel _teacherViewModel;
        public TeacherViewModel TeacherViewModel
        {
            get => _teacherViewModel;
            set => this.RaiseAndSetIfChanged(ref _teacherViewModel, value);
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

        private string _popupMessage;
        public string PopupMessage
        {
            get => _popupMessage;
            set => this.RaiseAndSetIfChanged(ref _popupMessage, value);
        }

        #endregion

        #region Functions

        public void Delete()
        {
            if (CurrentStudent.Id != null)
            {
                _studentService.Delete((int)CurrentStudent.Id);
                Students = _studentService.GetAll().ToArray();
                ClosePopup();
            }
        }

        public void Search()
        {
            Students = _studentService.Search(SearchInput).ToArray();
        }

        public void Insert()
        {
            MainViewModel.Instance.SetCurrentViewModel(new StudentEditorViewModel(null));
        }

        public void Update()
        {
            if (CurrentStudent.Id != null)
                MainViewModel.Instance.SetCurrentViewModel(new StudentEditorViewModel(CurrentStudent.Id));
        }


        private void OpenPopup()
        {
            if (CurrentStudent.Id == null) return;

            IsPopupOpen = true;
            PopupMessage = "Are you sure to delete this student?";
        }

        private void ClosePopup() => IsPopupOpen = false;

        #endregion


        #region Commands

        private VoidReactiveCommand _insertStudentCommand;
        public VoidReactiveCommand InsertStudentCommand =>
            _insertStudentCommand ??= VoidReactiveCommand.Create(Insert);


        private VoidReactiveCommand _updateStudentCommand;
        public VoidReactiveCommand UpdateStudentCommand =>
            _updateStudentCommand ??= VoidReactiveCommand.Create(Update);


        private VoidReactiveCommand _deleteCommand;
        public VoidReactiveCommand DeleteCommand =>
            _deleteCommand ??= VoidReactiveCommand.Create(Delete);


        private VoidReactiveCommand _searchCommand;
        public VoidReactiveCommand SearchCommand =>
            _searchCommand ??= VoidReactiveCommand.Create(Search);

        private VoidReactiveCommand _openPopupCommand;
        public VoidReactiveCommand OpenPopupCommand =>
            _openPopupCommand ??= VoidReactiveCommand.Create(OpenPopup);

        private VoidReactiveCommand _cancelCommand;
        public VoidReactiveCommand CancelCommand =>
            _cancelCommand ??= VoidReactiveCommand.Create(ClosePopup);

        #endregion

    }
}
