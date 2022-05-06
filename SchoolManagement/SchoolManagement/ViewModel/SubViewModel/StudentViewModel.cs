using System;
using System.Collections.Generic;
using System.Data;
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

        private readonly StudentService _studentService = new StudentService();

        #endregion

        public StudentViewModel()
        {
            _teacherViewModel = new TeacherViewModel();
            CurrentStudent = new Student();
            Students = _studentService.GetAll(true).ToArray();
            GenerateStudentDataTable();
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
            if (CurrentStudent.Id != 0)
            {
                _studentService.Delete((int)CurrentStudent.Id);
                Students = _studentService.GetAll(false).ToArray();
                ClosePopup();
            }
        }

        public void Search()
        {
            Students = _studentService.Search(SearchInput, false).ToArray();
        }

        public void Insert()
        {
            MainViewModel.Instance.SetCurrentViewModel(new StudentEditorViewModel(0));
        }

        public void Update()
        {
            if (CurrentStudent.Id != 0)
                MainViewModel.Instance.SetCurrentViewModel(new StudentEditorViewModel(CurrentStudent.Id));
        }


        private void OpenPopup()
        {
            if (CurrentStudent != null && CurrentStudent.Id == 0) return;

            IsPopupOpen = true;
            PopupMessage = "Are you sure to delete this student?";
        }

        private void ClosePopup() => IsPopupOpen = false;

        public DataTable GenerateStudentDataTable()
        {
            var table = new DataTable("Students");
            table.Columns.Add("Id");
            table.Columns.Add("Name");
            table.Columns.Add("Surname");
            table.Columns.Add("BirthDate");
            table.Columns.Add("RegisterDate");
            table.Columns.Add("Teachers");

            foreach (var student in Students)
            {
                DataRow row = table.NewRow();
                row["Id"] = student.Id;
                row["Name"] = student.Name;
                row["Surname"] = student.Surname;
                row["BirthDate"] = student.BirthDate;
                row["RegisterDate"] = student.RegisterDate;

                var teachers = student.Teachers.Select(t => t.Name + " " + t.Surname);
                row["Teachers"] = string.Join(",", teachers.ToArray());

                table.Rows.Add(row);
            }

            return table;
        }

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
