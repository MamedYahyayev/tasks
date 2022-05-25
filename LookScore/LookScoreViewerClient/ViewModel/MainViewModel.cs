using ReactiveUI;

namespace LookScoreViewerClient.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        #region Private Properties

        private string[] _games;

        #endregion

        public MainViewModel()
        {
            LookScoreWCF.GameServiceClient client = new LookScoreWCF.GameServiceClient();
            Games = client.GetAllGamesTitle();
        }

        #region Public Properties

        public string[] Games
        {
            get => _games;
            set => this.RaiseAndSetIfChanged(ref _games, value);
        }

        #endregion
    }
}
