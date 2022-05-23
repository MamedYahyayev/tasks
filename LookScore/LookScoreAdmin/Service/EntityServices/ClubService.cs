using LookScoreAdmin.Model.Entity;
using LookScoreAdmin.Service.FileServices;
using System;

namespace LookScoreAdmin.Service.EntityServices
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

        public void Insert(Club entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Club entity)
        {
            throw new NotImplementedException();
        }
    }
}
