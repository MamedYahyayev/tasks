using LookScoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReactiveUI;

namespace LookScoreViewerClient.ViewModel
{
    public class GameListViewModel : ReactiveObject
    {
        public GameListViewModel()
        {
            Games = new Game[25];
        }

        #region Public Properties

        private Game[] _games;
        public Game[] Games
        {
            get => _games;
            set => this.RaiseAndSetIfChanged(ref _games, value);
        }

        #endregion

    }
}
