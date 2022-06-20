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
        Game InsertGame(Game game);

        [OperationContract]
        void StartGame(Game game);

        [OperationContract]
        void StopGame(Game game);
    }
}
