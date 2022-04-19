using ReactiveUI;
using SchoolManagement.Command;
using SchoolManagement.Enum;
using SchoolManagement.Service;
using SchoolManagement.Utility;
using SchoolManagement.ViewModel.SubViewModel;
using System;

namespace SchoolManagement.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Private Properties

        public static MainViewModel Instance { get; private set; }

        #endregion


        public MainViewModel()
        {
            Instance = this;

            CurrentViewModel = new StudentViewModel();
        }

        #region Public Properties

        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
        }

        #endregion


        #region Functions

        private void CurrentViewChange(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.STUDENT:
                    CurrentViewModel = new StudentViewModel();
                    break;
                case ViewType.TEACHER:
                    CurrentViewModel = new TeacherViewModel();
                    break;
                case ViewType.STUDENT_TEACHERS:
                    CurrentViewModel = new StudentTeachersViewModel();
                    break;
            }
        }

        public void SetCurrentViewModel(BaseViewModel viewModel)
        {
            CurrentViewModel = viewModel;
        }

        #endregion

        #region Commands

        private VoidReactiveCommand<ViewType> _currentViewChangeCommand;
        public VoidReactiveCommand<ViewType> CurrentViewChangeCommand =>
            _currentViewChangeCommand ??= VoidReactiveCommand<ViewType>.Create(CurrentViewChange);

        #endregion

    }
}
