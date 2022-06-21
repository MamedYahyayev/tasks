using LookScoreCommon.Model;
using LookScoreServer.Service.FileServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LookScoreServer.Repository
{
    public class GameRepository : ICrudRepository<Game>
    {
        private readonly ClubRepository _clubRepository;

        public GameRepository()
        {
            _clubRepository = new ClubRepository();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Game[] FindAll()
        {
            return DataService.Instance.Storage.Games ?? new Game[0];
        }

        public Game FindOne(int id)
        {
            var game = DataService.Instance.Storage.Games.First(g => g.Id == id);

            game.HomeClub = _clubRepository.FindOne(game.HomeClubId);
            game.GuestClub = _clubRepository.FindOne(game.GuestClubId);

            return game;
        }

        public void Insert(Game game)
        {
            InsertGameIntoDataService(game);
        }

        public Game InsertAndReturn(Game game)
        {
            return InsertGameIntoDataService(game);
        }

        public void Update(Game entity)
        {
            throw new NotImplementedException();
        }

        public Game[] FindAllDetails()
        {
            var games = DataService.Instance.Storage.Games;
            foreach (var game in games)
            {
                game.HomeClub = _clubRepository.FindOne(game.HomeClubId);
                game.GuestClub = _clubRepository.FindOne(game.GuestClubId);
            }

            return games ?? new Game[0];
        }

        public void StartGame(Game game)
        {
            var games = DataService.Instance.Storage.Games;

            int index = Array.FindIndex(games, x => x.Id == game.Id);
            var findedGame = games[index];

            findedGame.GameStartDate = game.GameStartDate;

            DataService.Instance.Storage.Games[index] = findedGame;
            DataService.Instance.SetStorageModified();
        }

        #region Helper Functions

        private int GetNextId()
        {
            return DataService.Instance.Storage.Games.Length + 1;
        }

        private Game InsertGameIntoDataService(Game game)
        {
            if (game == null) return null;

            game.Id = GetNextId();
            game.GameTitle = game.HomeClub.Name + "-" + game.GuestClub.Name;
            game.HomeClubId = game.HomeClub.Id;
            game.GuestClubId = game.GuestClub.Id;

            List<Game> games = new List<Game>(DataService.Instance.Storage.Games);
            games.Add(game);
            DataService.Instance.Storage.Games = games.ToArray();
            DataService.Instance.SetStorageModified();

            return game;
        }

        #endregion
    }
}
