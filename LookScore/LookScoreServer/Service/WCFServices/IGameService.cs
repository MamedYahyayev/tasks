using LookScoreServer.Model.Entity;
using System.ServiceModel;

namespace LookScoreServer.Service.WCFServices
{
    [ServiceContract]
    public interface IGameService
    {
        [OperationContract]
        string[] GetAllGamesTitle();

        [OperationContract]
        Game[] FindAllGames();
    }
}
