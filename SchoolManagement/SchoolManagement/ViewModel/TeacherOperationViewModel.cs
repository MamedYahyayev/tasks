using ReactiveUI;
using SchoolManagement.Command;
using SchoolManagement.Enum;
using SchoolManagement.Model;
using SchoolManagement.Service;
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

            // load subject enums 

            if (id != null) LoadTeacher(id);
            else Teacher = new Teacher();

        }

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


        #region Public Properties

        private Teacher _teacher;
        public Teacher Teacher
        {
            get => _teacher;
            set => this.RaiseAndSetIfChanged(ref _teacher, value);
        }

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
            // create method that deletes error message for specific key if it has error

            if (string.IsNullOrEmpty(Teacher.Name))
            {
                var errorModel = new ErrorModel()
                {
                    HasError = true,
                    ErrorMessage = "Name cannot be empty"
                };

                //Errors.Add(nameof(Student.Name), errorModel);

            }

            if (string.IsNullOrEmpty(Teacher.Surname))
            {
                var errorModel = new ErrorModel()
                {
                    HasError = true,
                    ErrorMessage = "Surname cannot be empty"
                };

                //Errors.Add(nameof(Student.Surname), errorModel);
            }



            return Teacher != null && !string.IsNullOrEmpty(Teacher.Name) && !string.IsNullOrEmpty(Teacher.Surname);
        }

        private void UnselectSubject()
        {
            Subject = null;
        }

        private void CancelOperation() => _callback?.Invoke(ViewType.TEACHER);

        #endregion

    }
}
