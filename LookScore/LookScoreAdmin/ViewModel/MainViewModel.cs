using System;
using ReactiveUI;

using LookScoreAdmin.Command;
using LookScoreAdmin.ViewModel.SubViewModel;
using LookScoreCommon.Enums;

namespace LookScoreAdmin.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Private Properties

        private BaseViewModel _currentView;
        public static MainViewModel Instance { get; private set; }

        #endregion

        public MainViewModel()
        {
            Instance = this;

            CurrentView = new GameViewModel();
        }


        #region Public Properties

        public BaseViewModel CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
        }

        #endregion


        #region Functions

        private void CurrentViewChange(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.GAME:
                    CurrentView = new GameViewModel();
                    break;
                case ViewType.CLUB:
                    CurrentView = new ClubViewModel();
                    break;
            }
        }

        public void SetCurrentViewModel(BaseViewModel viewModel)
        {
            CurrentView = viewModel;
        }

        #endregion


        #region Commands

        private readonly VoidReactiveCommand<ViewType> _currentViewChangeCommand;
        public VoidReactiveCommand<ViewType> CurrentViewChangeCommand =>
            _currentViewChangeCommand ?? VoidReactiveCommand<ViewType>.Create(CurrentViewChange);

        #endregion
    }
}
