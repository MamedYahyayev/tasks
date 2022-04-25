using ReactiveUI;
using SchoolManagement.Command;
using SchoolManagement.Enum;
using SchoolManagement.Utility;
using SchoolManagement.ViewModel.SubViewModel;
using System.Windows;

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

        private WindowState _windowState;
        public WindowState WindowState
        {
            get => _windowState;
            set => this.RaiseAndSetIfChanged(ref _windowState, value);
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

        private void MinimizeWindow()
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeWindow()
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void CloseWindow(IClosable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }


        #endregion

        #region Commands

        private VoidReactiveCommand<ViewType> _currentViewChangeCommand;
        public VoidReactiveCommand<ViewType> CurrentViewChangeCommand =>
            _currentViewChangeCommand ??= VoidReactiveCommand<ViewType>.Create(CurrentViewChange);

        private VoidReactiveCommand _minimizeWindowCommand;
        public VoidReactiveCommand MinimizeWindowCommand =>
            _minimizeWindowCommand ??= VoidReactiveCommand.Create(MinimizeWindow);

        private VoidReactiveCommand _maximizeWindowCommand;
        public VoidReactiveCommand MaximizeWindowCommand =>
            _maximizeWindowCommand ??= VoidReactiveCommand.Create(MaximizeWindow);

        private VoidReactiveCommand<IClosable> _closeWindowCommand;
        public VoidReactiveCommand<IClosable> CloseWindowCommand =>
            _closeWindowCommand ??= VoidReactiveCommand<IClosable>.Create(CloseWindow);

        #endregion

    }
}
