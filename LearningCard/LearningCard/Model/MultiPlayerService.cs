using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Runtime.Serialization.Json;
using System.IO;
using LearningCardClasses;

namespace LearningCard.Model
{
    [ServiceContract]
    internal interface IMultiplayerService
    {
        [OperationContract]
        void JoinToGame();
    }

    [ServiceContract]
    class MultiPlayerService
    {
        
        private List<Profile> ActivePlayers;
        private QnAModel SingleModel;

        public MultiPlayerService(String fileName)
        {
            this.SingleModel = new QnAModel(fileName);
        }

        public void JoinToGame(Profile profile)
        {
            this.ActivePlayers.Add(profile);
        }
    }
}
