using LookScoreInterfaces.Model.Entity;
using LookScoreInterfaces.Service.FileServices;
using System;
using System.Collections.Generic;

namespace LookScoreInterfaces.Service.EntityServices
{
    public class GameService : ICrudOperation<Game>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Game[] FindAll()
        {
            //return DataService.Instance.Storage.Games ?? new Game[0];
            return new Game[0];
        }

        public Game FindOne(int id)
        {
            throw new NotImplementedException();
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

        #region Helper Functions

        private int GetNextId()
        {
            return DataService.Instance.Storage.Games.Length + 1;
        }

        #endregion
    }
}
