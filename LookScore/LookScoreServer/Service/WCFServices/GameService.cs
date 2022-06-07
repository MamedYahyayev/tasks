using LookScoreCommon.Model;
using System.Linq;

namespace LookScoreServer.Service.WCFServices
{
    public class GameService : IGameService
    {
        private readonly EntityServices.GameService _gameService;

        public GameService()
        {
            _gameService = new EntityServices.GameService();
        }

        public Game[] FindAllGameDetails()
        {
            return _gameService.FindAllDetails();
        }

        public Game[] FindAllGames()
        {
            return _gameService.FindAll();
        }

        public string[] GetAllGamesTitle()
        {
            return _gameService.FindAll().Select(g => g.GameTitle).ToArray();
        }

        public void InsertGame(Game game)
        {
            _gameService.Insert(game);
        }
    }
}
