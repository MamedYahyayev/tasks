﻿using ReactiveUI;
using SchoolManagement.Command;
using SchoolManagement.Config;
using SchoolManagement.Enum;
using SchoolManagement.Model;
using SchoolManagement.Model.Enum;
using SchoolManagement.Service;
using SchoolManagement.ViewModel.SubViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        #region Private Properties
        private readonly TeacherService _teacherService = new TeacherService();

        #endregion


        public MainViewModel()
        {
            StudentViewModel = new StudentViewModel();

            CurrentView = StudentViewModel;
        }

        #region Public Properties

        public StudentViewModel StudentViewModel { get; set; }
        public TeacherViewModel TeacherViewModel { get; set; }

        private object _currentView;
        public Object CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
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

        private void CurrentViewChange(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.STUDENT:
                    StudentViewModel = new StudentViewModel();
                    CurrentView = StudentViewModel;
                    break;
                case ViewType.TEACHER:
                    TeacherViewModel = new TeacherViewModel();
                    CurrentView = TeacherViewModel;
                    break;
            }
        }

        private void InsertPerson()
        {
            if (IsCurrentViewStudent())
                CurrentView = new StudentEditorViewModel(null, CurrentViewChange);
            else if (IsCurrentViewTeacher())
                CurrentView = new TeacherEditorViewModel(null, CurrentViewChange);
        }

        private void UpdatePerson()
        {
            if (IsCurrentViewStudent() && IsStudentSelected())
                CurrentView = new StudentEditorViewModel(StudentViewModel.CurrentStudent.Id, CurrentViewChange);
            else if (IsCurrentViewTeacher() && IsTeacherSelected())
                CurrentView = new TeacherEditorViewModel(TeacherViewModel.CurrentTeacher.Id, CurrentViewChange);

        }

        private void DeletePerson(ITableOperation operation)
        {

            operation = (ITableOperation)CurrentView;

            if (IsStudentSelected())
                operation.Delete((int)StudentViewModel.CurrentStudent.Id);

            else if (IsTeacherSelected())
                operation.Delete((int)TeacherViewModel.CurrentTeacher.Id);

            IsPopupOpen = false;
        }

        private void SearchPerson(ITableOperation operation)
        {
            operation = (ITableOperation)CurrentView;
            operation.Search(SearchInput);
        }


        private void OpenPopup()
        {
            IsPopupOpen = true;
            PopupMessage = "Are you sure to delete?";

            if (IsTeacherSelected())
            {
                var studentCount = _teacherService.FindStudentCount((int)TeacherViewModel.CurrentTeacher?.Id);
                if (studentCount > 0)
                {
                    var teacher = TeacherViewModel.CurrentTeacher;
                    PopupMessage = teacher.Name + " " + teacher.Surname + " has " + studentCount + " students. " +
                        " If you want to delete teacher all of his students will be deleted. Do you want to continue?";
                }
            } else
            {
                PopupMessage = "Are you sure to delete?";
            }
        }

        private void CancelPopup()
        {
            IsPopupOpen = false;
        }

        private bool IsCurrentViewStudent() => CurrentView is StudentViewModel;
        private bool IsCurrentViewTeacher() => CurrentView is TeacherViewModel;
        private bool IsStudentSelected() => StudentViewModel?.CurrentStudent?.Id != null;
        private bool IsTeacherSelected() => TeacherViewModel?.CurrentTeacher?.Id != null;


        #endregion

        #region Commands

        private VoidReactiveCommand<ViewType> _currentViewChangeCommand;
        public VoidReactiveCommand<ViewType> CurrentViewChangeCommand =>
            _currentViewChangeCommand ??= VoidReactiveCommand<ViewType>.Create(CurrentViewChange);

        private VoidReactiveCommand _insertPersonCommand;
        public VoidReactiveCommand InsertPersonCommand =>
            _insertPersonCommand ??= VoidReactiveCommand.Create(InsertPerson);


        private VoidReactiveCommand _updatePersonCommand;
        public VoidReactiveCommand UpdatePersonCommand =>
            _updatePersonCommand ??= VoidReactiveCommand.Create(UpdatePerson);


        private VoidReactiveCommand<ITableOperation> _deletePersonCommand;
        public VoidReactiveCommand<ITableOperation> DeletePersonCommand =>
            _deletePersonCommand ??= VoidReactiveCommand<ITableOperation>.Create(DeletePerson);


        private VoidReactiveCommand<ITableOperation> _searchCommand;
        public VoidReactiveCommand<ITableOperation> SearchCommand =>
            _searchCommand ??= VoidReactiveCommand<ITableOperation>.Create(SearchPerson);

        private VoidReactiveCommand _openPopupCommand;
        public VoidReactiveCommand OpenPopupCommand =>
            _openPopupCommand ??= VoidReactiveCommand.Create(OpenPopup);

        private VoidReactiveCommand _closePopupCommand;
        public VoidReactiveCommand ClosePopupCommand =>
            _closePopupCommand ??= VoidReactiveCommand.Create(CancelPopup);

        #endregion

    }
}
