using LookScoreAdmin.Command;
using LookScoreServer.Model.Entity;
using LookScoreServer.Service.EntityServices;
using ReactiveUI;

namespace LookScoreAdmin.ViewModel.SubViewModel
{
    public class ClubViewModel : BaseViewModel
    {
        #region Private Properties

        private Club[] _clubs;
        private readonly ClubService _clubService = new ClubService();

        #endregion


        public ClubViewModel()
        {
            Clubs = _clubService.FindAll();
        }

        #region Public Properties

        public Club[] Clubs
        {
            get => _clubs;
            set => this.RaiseAndSetIfChanged(ref _clubs, value);
        }

        #endregion


        #region Functions


        private void OpenNewClubView()
        {
            MainViewModel.Instance.SetCurrentViewModel(new ClubEditorViewModel());
        }

        #endregion


        #region Commands

        private readonly VoidReactiveCommand _newClubViewCommand;
        public VoidReactiveCommand NewClubCommand =>
            _newClubViewCommand ?? VoidReactiveCommand.Create(OpenNewClubView);



        #endregion

    }
}
