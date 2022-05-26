//using LookScoreInterfaces.Model.Entity;
using LookScoreServer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LookScoreWCF
{
    [ServiceContract]
    public interface IGameService
    {
        //[OperationContract]
        //string[] GetAllGamesTitle();

        //[OperationContract]
        //Game[] FindAllGames();
    }


}
