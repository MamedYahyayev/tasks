using ReactiveUI;
using SchoolManagement.Command;
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
        private readonly TeacherService _teacherService = new TeacherService();

        private TeacherListViewModel _teacherViewModel = new TeacherListViewModel();
        private Teacher _teacher = new Teacher();
        private Action<string> _callback;
        private string _operationType;


        public TeacherOperationViewModel(string operationType, Action<string> callback, int? id = null)
        {
            _operationType = operationType;
            _callback = callback;

            if (operationType == "UPDATE")
            {
                TeacherView.CurrentTeacher = _teacherService.GetById((int)id);
                Teacher = TeacherView.CurrentTeacher;
                // current subject
            }

        }

        public Teacher Teacher
        {
            get => _teacher;
            set => this.RaiseAndSetIfChanged(ref _teacher, value);
        }

        public TeacherListViewModel TeacherView
        {
            get => _teacherViewModel;
            set => this.RaiseAndSetIfChanged(ref _teacherViewModel, value);
        }

        private void CancelOperation() => _callback?.Invoke("TEACHER");

        private void ProcessOperation()
        {
            if (_operationType == "INSERT")
                InsertTeacher();
            else
                UpdateTeacher();

            _callback?.Invoke("STUDENT");
        }

        private void InsertTeacher()
        {
            if (!IsDataValid()) return;

            // set subject in here

            _teacherService.Insert(Teacher);
        }

        private void UpdateTeacher()
        {
            if (!IsDataValid()) return;

            // set subject here

            _teacherService.Update(Teacher);
        }

        private bool IsDataValid()
        {
            return Teacher != null && !string.IsNullOrEmpty(Teacher.Name) && !string.IsNullOrEmpty(Teacher.Surname);
        }

        private void UnselectSubject()
        {
            TeacherView.CurrentTeacher = null;
            TeacherView.CurrentTeacher = new Teacher();
        }

        private VoidReactiveCommand _processOperationCommand;
        public VoidReactiveCommand ProcessOperationCommand =>
            _processOperationCommand ??= VoidReactiveCommand.Create(ProcessOperation);


        private VoidReactiveCommand _cancelOperationCommand;
        public VoidReactiveCommand CancelOperationCommand =>
            _cancelOperationCommand ??= VoidReactiveCommand.Create(CancelOperation);

        private VoidReactiveCommand _unselectCommand;
        public VoidReactiveCommand UnselectCommand =>
            _unselectCommand ??= VoidReactiveCommand.Create(UnselectSubject);
    }
}
