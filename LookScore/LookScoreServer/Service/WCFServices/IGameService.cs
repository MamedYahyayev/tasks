using LookScoreCommon.Model;
using System.ServiceModel;

namespace LookScoreServer.Service.WCFServices
{
    [ServiceContract(CallbackContract = typeof(IGameCallbackService))]
    public interface IGameService
    {
        [OperationContract]
        void JoinToChannel();

        [OperationContract]
        string[] GetAllGamesTitle();

        [OperationContract]
        Game[] FindAllGames();

        [OperationContract]
        Game[] FindAllGameDetails();

        [OperationContract]
        void InsertGame(Game game);

        [OperationContract]
        void StartGame(Game game);
    }
}
