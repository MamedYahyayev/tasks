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
    public class StudentOperationViewModel : ReactiveObject
    {
        #region Properties

        private Student _student;
        public Student Student
        {
            get => _student;
            set => this.RaiseAndSetIfChanged(ref _student, value);
        }

        private Teacher[] _allTeachers;
        public Teacher[] AllTeachers
        {
            get => _allTeachers;
            set => this.RaiseAndSetIfChanged(ref _allTeachers, value);

        }

        private Teacher _selectedTeacher;
        public Teacher SelectedTeacher
        {
            get => _selectedTeacher;
            set => this.RaiseAndSetIfChanged(ref _selectedTeacher, value);
        }

        private StudentListViewModel _studentViewModel;
        public StudentListViewModel StudentViewModel
        {
            get => _studentViewModel;
            set => this.RaiseAndSetIfChanged(ref _studentViewModel, value);
        }

        private readonly StudentService _studentService = new StudentService();

        private Action<ViewType> _callback;

        private TeacherService _teacherService;

        #endregion


        public StudentOperationViewModel(int? id, Action<ViewType> callback)
        {
            _callback = callback;
            LoadAllTeachers();
            if (id != null) LoadStudent(id);
            else Student = new Student();
        }

        #region Functions

        private void LoadAllTeachers()
        {
            _teacherService = new TeacherService();
            var teachers = _teacherService.GetAll();
            AllTeachers = teachers.ToArray();
        }

        private void LoadStudent(int? id)
        {
            var student = _studentService.GetById((int)id);
            Student = student;
            SelectedTeacher = AllTeachers?.FirstOrDefault(t => t.Id == student.Teacher.Id);
        }

        private void Save(int? id)
        {
            if (id == null)
                InsertStudent();
            else
                UpdateStudent(Student);
        }

        private void UpdateStudent(Student student)
        {
            if (!IsDataValid()) return;

            Student.Teacher = SelectedTeacher;
            var isUpdated = _studentService.Update(student);
            if (isUpdated) CancelOperation();
        }

        private void InsertStudent()
        {
            if (!IsDataValid()) return;

            Student.Teacher = SelectedTeacher;

            var isAdded = _studentService.Insert(Student);
            if (isAdded) CancelOperation();
        }

        private bool IsDataValid()
        {
            return Student != null && !string.IsNullOrEmpty(Student.Name) && !string.IsNullOrEmpty(Student.Surname);
        }

        private void UnselectTeacher()
        {
            SelectedTeacher = null;
        }

        private void CancelOperation() => _callback?.Invoke(ViewType.STUDENT);

        #endregion

        #region Commands
        private VoidReactiveCommand<int?> _saveCommand;
        public VoidReactiveCommand<int?> SaveCommand =>
            _saveCommand ??= VoidReactiveCommand<int?>.Create(Save);

        private VoidReactiveCommand _cancelOperationCommand;
        public VoidReactiveCommand CancelOperationCommand =>
            _cancelOperationCommand ??= VoidReactiveCommand.Create(CancelOperation);

        private VoidReactiveCommand _unselectCommand;
        public VoidReactiveCommand UnselectCommand =>
            _unselectCommand ??= VoidReactiveCommand.Create(UnselectTeacher);

        #endregion

    }
}
