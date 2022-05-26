using LookScoreServer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LookScoreServer.Service.WCFServices
{
    public class ClubService : IClubService
    {
        private readonly EntityServices.ClubService _clubService;

        public ClubService()
        {
            _clubService = new EntityServices.ClubService();
        }

        public Club[] FindAllClubs()
        {
            return _clubService.FindAll();
        }
    }
}
