using LookScoreCommon.Model;
using LookScoreServer.Service.FileServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LookScoreServer.Service.EntityServices
{
    public class GameService : ICrudOperation<Game>
    {
        private readonly ClubService _clubService;

        public GameService()
        {
            _clubService = new ClubService();
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
            return DataService.Instance.Storage.Games.First(g => g.Id == id);
        }

        public void Insert(Game game)
        {
            if (game == null) return;

            game.Id = GetNextId();
            game.GameTitle = game.HomeClub.Name + "-" + game.GuestClub.Name;
            game.HomeClubId = game.HomeClub.Id;
            game.GuestClubId = game.GuestClub.Id;

            List<Game> games = new List<Game>(DataService.Instance.Storage.Games);
            games.Add(game);
            DataService.Instance.Storage.Games = games.ToArray();
            DataService.Instance.SetStorageModified();
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
                game.HomeClub = _clubService.FindOne(game.HomeClubId);
                game.GuestClub = _clubService.FindOne(game.GuestClubId);
            }

            return games ?? new Game[0];
        }

        #region Helper Functions

        private int GetNextId()
        {
            return DataService.Instance.Storage.Games.Length + 1;
        }

        #endregion
    }
}
