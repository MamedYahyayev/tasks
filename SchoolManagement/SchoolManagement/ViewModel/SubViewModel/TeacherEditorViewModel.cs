using ReactiveUI;
using SchoolManagement.Command;
using SchoolManagement.Enum;
using SchoolManagement.Model;
using SchoolManagement.Service;
using SchoolManagement.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.ViewModel.SubViewModel
{
    public class TeacherEditorViewModel : BaseViewModel
    {

        #region Private Properties

        private readonly TeacherService _teacherService = new TeacherService();

        #endregion

        public TeacherEditorViewModel(int id)
        {
            _errors = new Dictionary<string, ErrorModel>();

            if (id != 0) LoadTeacher(id);
            else Teacher = new Teacher();

        }

        #region Public Properties

        public Subject[] AllSubjects => System.Enum.GetValues(typeof(Subject)).Cast<Subject>().ToArray();

        public Subject? Subject
        {
            get
            {
                if (Teacher.Subject == null) { return null; }
                else return (Subject)Teacher.Subject;
            }
            set => Teacher.Subject = value;
        }


        private Teacher _teacher;
        public Teacher Teacher
        {
            get => _teacher;
            set => this.RaiseAndSetIfChanged(ref _teacher, value);
        }

        private Dictionary<string, ErrorModel> _errors;
        public Dictionary<string, ErrorModel> Errors
        {
            get => _errors;
            set => this.RaiseAndSetIfChanged(ref _errors, value);
        }

        private string _salary;
        public string Salary
        {
            get => _salary;
            set => this.RaiseAndSetIfChanged(ref _salary, value);
        }

        #endregion


        #region Functions

        private void LoadTeacher(int id)
        {
            Teacher = _teacherService.GetById(id, false);
            Salary = Teacher.Salary.ToString();
        }


        private void Save(int id)
        {
            if (id == 0)
                InsertTeacher();
            else
                UpdateTeacher(Teacher);
        }

        private void UpdateTeacher(Teacher teacher)
        {
            if (!IsDataValid()) return;

            if (Subject != null) Teacher.Subject = Subject;

            _teacherService.Update(Teacher);
            CancelOperation();

        }

        private void InsertTeacher()
        {
            if (!IsDataValid()) return;

            if (Subject != null)
                Teacher.Subject = Subject;

            _teacherService.Insert(Teacher);
            CancelOperation();
        }

        private bool IsDataValid()
        {
            Errors.Clear();

            this.RaisePropertyChanged(nameof(Errors));

            Errors = PersonValidator.ValidatePerson(Teacher, Errors);

            var errorModel = PersonValidator.CheckEmpty(nameof(Teacher.License), Teacher.License);
            if (errorModel != null) Errors.Add(nameof(Teacher.License), errorModel);

            errorModel = PersonValidator.IsNumberAndPositive(nameof(Teacher.Salary), Salary);
            if (errorModel != null) Errors.Add(nameof(Teacher.Salary), errorModel);
            else Teacher.Salary = double.Parse(Salary);

            this.RaisePropertyChanged(nameof(Errors));

            return Teacher != null && Errors.Count == 0;
        }


        private void UnselectSubject() => Subject = null;

        private void CancelOperation() => MainViewModel.Instance.SetCurrentViewModel(new TeacherViewModel());

        #endregion


        #region Commands


        private VoidReactiveCommand<int> _saveCommand;
        public VoidReactiveCommand<int> SaveCommand =>
            _saveCommand ??= VoidReactiveCommand<int>.Create(Save);


        private VoidReactiveCommand _cancelCommand;
        public VoidReactiveCommand CancelCommand =>
            _cancelCommand ??= VoidReactiveCommand.Create(CancelOperation);

        private VoidReactiveCommand _unselectCommand;
        public VoidReactiveCommand UnselectCommand =>
            _unselectCommand ??= VoidReactiveCommand.Create(UnselectSubject);

        #endregion


    }
}
