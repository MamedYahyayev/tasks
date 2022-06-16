using LookScoreAdmin.Command;
using LookScoreCommon.Model;
using LookScoreServer.Service.WCFServices;
using ReactiveUI;
using System.ServiceModel;

namespace LookScoreAdmin.ViewModel.SubViewModel
{
    public class ClubViewModel : BaseViewModel
    {
        #region Private Properties

        private Club[] _clubs;

        #endregion


        public ClubViewModel()
        {
            ChannelFactory<IClubService> channel = new ChannelFactory<IClubService>("ClubService");
                
            Clubs = channel.CreateChannel().FindAllClubs();
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
