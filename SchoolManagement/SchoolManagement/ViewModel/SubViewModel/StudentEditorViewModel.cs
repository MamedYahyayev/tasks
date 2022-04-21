using ReactiveUI;
using SchoolManagement.Command;
using SchoolManagement.Enum;
using SchoolManagement.Model;
using SchoolManagement.Service;
using SchoolManagement.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.ViewModel.SubViewModel
{
    public class StudentEditorViewModel : BaseViewModel
    {
        #region Private Properties

        private readonly StudentService _studentService = new StudentService(new GeneralFileService().GetFileService<Student>(App.FILE_SERVICE));

        private TeacherService _teacherService;

        private List<Teacher> _selectedTeacherList = new List<Teacher>();

        #endregion

        public StudentEditorViewModel(int? id)
        {
            Errors = new Dictionary<string, ErrorModel>();

            LoadAllTeachers();
            if (id != null) LoadStudent(id);
            else Student = new Student();
        }

        #region Public Properties

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
            set =>
                this.RaiseAndSetIfChanged(ref _selectedTeacher, value);
        }

        private Teacher[] _selectedTeachers;
        public Teacher[] SelectedTeachers
        {
            get => _selectedTeachers;
            set => this.RaiseAndSetIfChanged(ref _selectedTeachers, value);
        }


        private Dictionary<string, ErrorModel> _errors;
        public Dictionary<string, ErrorModel> Errors
        {
            get => _errors;
            set => this.RaiseAndSetIfChanged(ref _errors, value);
        }

        #endregion


        #region Functions

        private void LoadAllTeachers()
        {
            _teacherService = new TeacherService(new GeneralFileService().GetFileService<Teacher>(App.FILE_SERVICE));
            var teachers = _teacherService.GetAll();
            AllTeachers = teachers.ToArray();
        }

        private void LoadStudent(int? id)
        {
            var student = _studentService.GetById((int)id);
            Student = student;
            // TODO:  check student teachers is null or not
            //SelectedTeachers = student.Teachers.ToArray();
            //_selectedTeacherList = student.Teachers.ToList();
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

            //Student.Teachers = _selectedTeachers;
            _studentService.Update(student);
            CancelOperation();
        }

        private void InsertStudent()
        {
            if (!IsDataValid()) return;

            //Student.Teachers = _selectedTeachers;
            _studentService.Insert(Student);
            CancelOperation();
        }

        private bool IsDataValid()
        {
            Errors.Clear();

            this.RaisePropertyChanged(nameof(Errors));

            Errors = PersonValidator.ValidatePerson(Student, Errors);

            this.RaisePropertyChanged(nameof(Errors));

            return Student != null && Errors.Count == 0;
        }


        private void CancelOperation() => MainViewModel.Instance.SetCurrentViewModel(new StudentViewModel());


        private void AssignTeacher()
        {
            if (SelectedTeacher == null) return;

            var isExist = _selectedTeacherList.Any(t => t.Id == SelectedTeacher.Id);
            if (!isExist)
            {
                _selectedTeacherList.Add(SelectedTeacher);
                SelectedTeachers = _selectedTeacherList.ToArray();
            }
        }

        private void UnassignTeacher()
        {
            if (SelectedTeacher == null) return;

            var isExist = _selectedTeacherList.Any(t => t.Id == SelectedTeacher.Id);
            if (isExist)
            {
                _selectedTeacherList = _selectedTeacherList.Where(t => t.Id != SelectedTeacher.Id).ToList();
                SelectedTeachers = _selectedTeacherList.ToArray();
            }
        }

        #endregion


        #region Commands

        private VoidReactiveCommand<int?> _saveCommand;
        public VoidReactiveCommand<int?> SaveCommand =>
            _saveCommand ??= VoidReactiveCommand<int?>.Create(Save);

        private VoidReactiveCommand _cancelOperationCommand;
        public VoidReactiveCommand CancelOperationCommand =>
            _cancelOperationCommand ??= VoidReactiveCommand.Create(CancelOperation);

        private VoidReactiveCommand _addTeacherCommand;
        public VoidReactiveCommand AddTeacherCommand =>
            _addTeacherCommand ??= VoidReactiveCommand.Create(AssignTeacher);

        private VoidReactiveCommand _removeTeacherCommand;
        public VoidReactiveCommand RemoveTeacherCommand =>
            _removeTeacherCommand ??= VoidReactiveCommand.Create(UnassignTeacher);

        #endregion

    }
}
