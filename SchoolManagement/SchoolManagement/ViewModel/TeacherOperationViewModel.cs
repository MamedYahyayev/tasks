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

namespace SchoolManagement.ViewModel
{
    public class TeacherOperationViewModel : ReactiveObject
    {

        #region Private Properties

        private readonly TeacherService _teacherService = new TeacherService();

        private Action<ViewType> _callback;

        #endregion

        public TeacherOperationViewModel(int? id, Action<ViewType> callback)
        {
            _callback = callback;
            _errors = new Dictionary<string, ErrorModel>();

            if (id != null) LoadTeacher(id);
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

        #endregion


        #region Functions

        private void LoadTeacher(int? id)
        {
            Teacher = _teacherService.GetById((int)id);
        }


        private void Save(int? id)
        {
            if (id == null)
                InsertTeacher();
            else
                UpdateTeacher(Teacher);
        }

        private void UpdateTeacher(Teacher teacher)
        {
            if (!IsDataValid()) return;

            if (Subject != null) Teacher.Subject = Subject;

            var isUpdated = _teacherService.Update(Teacher);
            if (isUpdated) CancelOperation();

        }

        private void InsertTeacher()
        {
            if (!IsDataValid()) return;

            if (Subject != null)
                Teacher.Subject = Subject;

            var isAdded = _teacherService.Insert(Teacher);
            if (isAdded) CancelOperation();
        }

        private bool IsDataValid()
        {
            Errors.Clear();

            this.RaisePropertyChanged(nameof(Errors));

            Errors = PersonValidator.ValidatePerson(Teacher, Errors);

            var errorModel = PersonValidator.CheckEmpty(nameof(Teacher.License), Teacher.License);
            if (errorModel != null) Errors.Add(nameof(Teacher.License), errorModel);

            this.RaisePropertyChanged(nameof(Errors));

            return Teacher != null && Errors.Count == 0;
        }


        private void UnselectSubject() => Subject = null;

        private void CancelOperation() => _callback?.Invoke(ViewType.TEACHER);

        #endregion


        #region Commands


        private VoidReactiveCommand<int?> _saveCommand;
        public VoidReactiveCommand<int?> SaveCommand =>
            _saveCommand ??= VoidReactiveCommand<int?>.Create(Save);


        private VoidReactiveCommand _cancelCommand;
        public VoidReactiveCommand CancelCommand =>
            _cancelCommand ??= VoidReactiveCommand.Create(CancelOperation);

        private VoidReactiveCommand _unselectCommand;
        public VoidReactiveCommand UnselectCommand =>
            _unselectCommand ??= VoidReactiveCommand.Create(UnselectSubject);

        #endregion


    }
}
