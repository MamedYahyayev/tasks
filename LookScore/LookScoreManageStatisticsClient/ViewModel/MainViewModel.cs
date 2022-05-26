using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using LookScoreServer.Model.Entity;
using LookScoreServer.Service.WCFServices;
using ReactiveUI;

namespace LookScoreManageStatisticsClient.ViewModel
{
    public class MainViewModel : ReactiveObject
    {
        #region Private Properties

        private Game[] _games;
        private Game _selectedGame;

        #endregion

        public MainViewModel()
        {
            ChannelFactory<IGameService> channelFactory = new ChannelFactory<IGameService>("GameService");
            IGameService gameService = channelFactory.CreateChannel();

            Games = gameService.FindAllGameDetails();
        }

        #region Public Properties

        public Game[] Games
        {
            get => _games;
            set => this.RaiseAndSetIfChanged(ref _games, value);
        }

        public Game SelectedGame
        {
            get => _selectedGame;
            set => this.RaiseAndSetIfChanged(ref _selectedGame, value);
        }

        #endregion



    }
}
