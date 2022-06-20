using GalaSoft.MvvmLight.Command;
using LookScoreCommon.Model;
using LookScoreCommon.ViewModel;
using LookScoreServer.Service.WCFServices;
using LookScoreViewerClient.Contract;
using ReactiveUI;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace LookScoreViewerClient.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            CurrentView = new GameListViewModel();
        }

        #region Public Properties

        private BaseViewModel _currentView;
        public BaseViewModel CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
        }

        #endregion

    }
}
