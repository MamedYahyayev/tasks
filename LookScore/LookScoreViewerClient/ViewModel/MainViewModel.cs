using LookScoreServer.Service.WCFServices;
using ReactiveUI;
using System.ServiceModel;

namespace LookScoreViewerClient.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        #region Private Properties

        private string[] _games;

        #endregion

        public MainViewModel()
        {
            ChannelFactory<IGameService> channelFactory = new ChannelFactory<IGameService>("LookScoreGameService");
            IGameService gameService = channelFactory.CreateChannel();

            Games = gameService.GetAllGamesTitle();
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
