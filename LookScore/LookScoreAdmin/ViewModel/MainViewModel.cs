using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LookScoreAdmin.Command;
using LookScoreAdmin.Model.Enums;
using LookScoreAdmin.ViewModel.SubViewModel;
using ReactiveUI;

namespace LookScoreAdmin.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Private Properties

        private BaseViewModel _currentView;

        #endregion

        public MainViewModel()
        {
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
                case ViewType.PLAYER:
                    CurrentView = new PlayerViewModel();
                    break;
                case ViewType.REFEREE:
                    CurrentView = new RefereeViewModel();
                    break;
                case ViewType.CLUB:
                    CurrentView = new ClubViewModel();
                    break;
            }
        }

        #endregion


        #region Commands

        private VoidReactiveCommand<ViewType> _currentViewChangeCommand;
        public VoidReactiveCommand<ViewType> CurrentViewChangeCommand =>
            _currentViewChangeCommand ?? VoidReactiveCommand<ViewType>.Create(CurrentViewChange);

        #endregion
    }
}
