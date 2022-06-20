using LookScoreCommon.Model;
using LookScoreServer.Repository;

namespace LookScoreServer.Service.WCFServices
{
    public class ClubService : IClubService
    {
        private readonly ClubRepository _clubRepository;

        public ClubService()
        {
            _clubRepository = new ClubRepository();
        }

        public Club[] FindAllClubs()
        {
            return _clubRepository.FindAll();
        }

        public void InsertClub(Club club)
        {
            _clubRepository.Insert(club);
        }
    }
}
