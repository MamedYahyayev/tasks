using LookScoreCommon.Model;
using LookScoreServer.Service.FileServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LookScoreServer.Repository
{
    public class ClubRepository : ICrudRepository<Club>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Club[] FindAll()
        {
            return DataService.Instance.Storage.Clubs ?? new Club[0];
        }

        public Club FindOne(int id)
        {
            return DataService.Instance.Storage.Clubs.First(x => x.Id == id);
        }

        public void Insert(Club club)
        {
            if (club == null) return;

            List<Club> clubs = new List<Club>(DataService.Instance.Storage.Clubs);
            club.Id = FindNextId();
            club.ShortName = club.Name.Substring(0, 3).ToUpper();
            clubs.Add(club);
            DataService.Instance.Storage.Clubs = clubs.ToArray();
            DataService.Instance.SetStorageModified();
        }

        public Club InsertAndReturn(Club entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Club entity)
        {
            throw new NotImplementedException();
        }

        #region Helper Functions

        private int FindNextId()
        {
            return DataService.Instance.Storage.Clubs.Length + 1;
        }

        #endregion
    }
}
