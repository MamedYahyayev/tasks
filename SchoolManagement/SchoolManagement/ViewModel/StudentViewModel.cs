using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using SchoolManagement.Model;
using SchoolManagement.Service;

namespace SchoolManagement.ViewModel
{
    public class StudentViewModel : ReactiveObject
    {
        private readonly StudentService _studentService;
        private IList<Student> _students;

        private int _id;
        private string _name;
        private string _surname;
        private DateTime _birthDate;
        private DateTime _registerDate;


        public StudentViewModel()
        {
            _studentService = new StudentService();
            Students = _studentService.GetAll();
        }

        public IList<Student> Students
        {
            get => _students;
            set => this.RaiseAndSetIfChanged(ref _students, value);
        }

        public int Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }

        public string Name
        {
            get => this._name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string Surname
        {
            get => this._surname;
            set => this.RaiseAndSetIfChanged(ref _surname, value);
        }

        public DateTime BirthDate
        {
            get => this._birthDate;
            set => this.RaiseAndSetIfChanged(ref this._birthDate, value);
        }

        public DateTime RegisterDate
        {
            get => this._registerDate;
            set => this.RaiseAndSetIfChanged(ref this._registerDate, value);
        }










    }
}
