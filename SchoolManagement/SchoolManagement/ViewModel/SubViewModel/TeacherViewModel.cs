using ReactiveUI;
using SchoolManagement.Command;
using SchoolManagement.Model;
using SchoolManagement.Model.Enum;
using SchoolManagement.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.ViewModel.SubViewModel
{
    public class TeacherViewModel : BaseViewModel
    {
        #region Private Properties

        private readonly TeacherService _teacherService;

        #endregion

        public TeacherViewModel()
        {
            _teacherService = new TeacherService();
            Teachers = _teacherService.GetAll(false).ToArray();
            CurrentTeacher = new Teacher();

            GenerateTeacherDataTable();
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

        private bool _isInfoPopupOpen;
        public bool IsInfoPopupOpen
        {
            get => _isInfoPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _isInfoPopupOpen, value);
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
            if (CurrentTeacher.Id != 0)
            {
                _teacherService.Delete((int)CurrentTeacher.Id);
                Teachers = _teacherService.GetAll(false).ToArray();
                ClosePopup();
            }
        }

        public void Search()
        {
            Teachers = _teacherService.Search(SearchInput, false).ToArray();
        }

        public void Insert()
        {
            MainViewModel.Instance.SetCurrentViewModel(new TeacherEditorViewModel(0));
        }

        public void Update()
        {
            if (CurrentTeacher.Id != 0)
                MainViewModel.Instance.SetCurrentViewModel(new TeacherEditorViewModel(CurrentTeacher.Id));
        }

        private void CalculateSalary()
        {
            var salary = Teachers.ToList().Sum(t => t.Salary);

            IsInfoPopupOpen = true;
            PopupMessage = "Total Salary is: " + salary;
        }


        private void OpenPopup()
        {
            if (CurrentTeacher.Id == 0) return;

            IsPopupOpen = true;
            PopupMessage = "Are you sure to delete?";
        }

        private void ClosePopup() => IsPopupOpen = false;
        private void CloseInfoPopup() => IsInfoPopupOpen = false;

        public DataTable GenerateTeacherDataTable()
        {
            var table = new DataTable("Teachers");
            table.Columns.Add("Id");
            table.Columns.Add("Name");
            table.Columns.Add("Surname");
            table.Columns.Add("BirthDate");
            table.Columns.Add("Salary");
            table.Columns.Add("Subject");

            foreach (var teacher in Teachers)
            {
                DataRow row = table.NewRow();
                row["Id"] = teacher.Id;
                row["Name"] = teacher.Name;
                row["Surname"] = teacher.Surname;
                row["BirthDate"] = teacher.BirthDate;
                row["Salary"] = teacher.Salary;
                row["Subject"] = teacher.Subject.GetName();

                table.Rows.Add(row);
            }

            return table;
        }


        #endregion

        #region Commands

        private VoidReactiveCommand _insertTeacherCommand;
        public VoidReactiveCommand InsertTeacherCommand =>
            _insertTeacherCommand ??= VoidReactiveCommand.Create(Insert);


        private VoidReactiveCommand _updateTeacherCommand;
        public VoidReactiveCommand UpdateTeacherCommand =>
            _updateTeacherCommand ??= VoidReactiveCommand.Create(Update);


        private VoidReactiveCommand _deleteCommand;
        public VoidReactiveCommand DeleteCommand =>
            _deleteCommand ??= VoidReactiveCommand.Create(Delete);

        private VoidReactiveCommand _okCommand;
        public VoidReactiveCommand OkCommand =>
            _okCommand  ??= VoidReactiveCommand.Create(CloseInfoPopup);


        private VoidReactiveCommand _searchCommand;
        public VoidReactiveCommand SearchCommand =>
            _searchCommand ??= VoidReactiveCommand.Create(Search);

        private VoidReactiveCommand _calculateSalaryCommand;
        public VoidReactiveCommand CalculateSalaryCommand =>
            _calculateSalaryCommand  ??= VoidReactiveCommand.Create(CalculateSalary);

        private VoidReactiveCommand _openPopupCommand;
        public VoidReactiveCommand OpenPopupCommand =>
            _openPopupCommand ??= VoidReactiveCommand.Create(OpenPopup);

        private VoidReactiveCommand _cancelCommand;
        public VoidReactiveCommand CancelCommand =>
            _cancelCommand  ??= VoidReactiveCommand.Create(ClosePopup);

        #endregion
    }
}
