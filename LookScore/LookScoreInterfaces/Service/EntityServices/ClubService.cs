using LookScoreInterfaces.Model.Entity;
using LookScoreInterfaces.Service.FileServices;
using System;
using System.Collections.Generic;

namespace LookScoreInterfaces.Service.EntityServices
{
    public class ClubService : ICrudOperation<Club>
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
            throw new NotImplementedException();
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
