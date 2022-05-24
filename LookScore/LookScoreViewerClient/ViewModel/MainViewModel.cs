using LookScoreAdmin.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
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
            GameService.ServiceClient serviceClient = new GameService.ServiceClient("GameServiceEndpoint");
            string[] result = serviceClient.FindAllGames();
            Games = result;
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
