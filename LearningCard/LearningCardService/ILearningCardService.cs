﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LearningCardClasses;

namespace LearningCardService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IServiceCallBack))]
    public interface ILearningCardService
    {
        [OperationContract(IsOneWay = true)]
        void RegisterClient(string clientName);

        [OperationContract(IsOneWay = true)]
        void NotifyServer(EventDataType eventData);

        [OperationContract]
        List<String> GetActivePlayers();
        [OperationContract(IsOneWay = true)]
        void StartGame(CardPack cp);
        [OperationContract(IsOneWay = true)]
        void SetVisibleDeckName(String name);
        [OperationContract]
        String GetVisibleDeckName();
        [OperationContract]
        CardPack GetServerDeck();
        [OperationContract(IsOneWay = true)]
        void SendAnswer(String clientName, Boolean IsRight);
        [OperationContract]
        Card GetCard();
        [OperationContract]
        Dictionary<String, int> GetScoreBoard();
    }

    public interface IServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void BroadcastToClient(EventDataType eventData);
    }
} 
