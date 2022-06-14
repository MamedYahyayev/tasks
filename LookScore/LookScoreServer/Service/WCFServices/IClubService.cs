using LookScoreCommon.Model;
using System.ServiceModel;

namespace LookScoreServer.Service.WCFServices
{
    [ServiceContract]
    public interface IClubService
    {
        [OperationContract]
        Club[] FindAllClubs();
    }
}
