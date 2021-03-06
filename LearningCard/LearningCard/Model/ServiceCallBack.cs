﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using LearningCardClasses;
using System.ServiceModel;

namespace LearningCard.Model
{
    class ServiceCallBack : LearningCard.OnlineLearningCardService.ILearningCardServiceCallback
    {

        protected OnlineLearningCardService.LearningCardServiceClient _client;
        private System.Threading.SynchronizationContext syncContext = AsyncOperationManager.SynchronizationContext;

        private EventHandler _serviceCallBackHandler;
        public void SetHandler(EventHandler handler)
        {
            this._serviceCallBackHandler = handler;
        }

        public void BroadcastToClient(EventDataType eventData)
        {
            syncContext.Post(new System.Threading.SendOrPostCallback(OnBroadcast), eventData);
        }

        private void OnBroadcast(object eventData)
        {
            this._serviceCallBackHandler.Invoke(eventData, null);
        }

        private delegate void HandleBroadcastCallback(object sender, EventArgs e);
        public virtual void HandleBroadcast(object sender, EventArgs e)
        {
            try
            {
                EventDataType eventData = (EventDataType)sender;
                    //eventData.EventMessage, eventData.ClientName);
            }
            catch (Exception)
            {
            }
        }

        public void RegisterClient()
        {
            if ((this._client != null))
            {
                this._client.Abort();
                this._client = null;
            }

            //ServiceCallBack cb = new ServiceCallBack();
            ServiceCallBack cb = this;
            cb.SetHandler(this.HandleBroadcast);

            System.ServiceModel.InstanceContext context =
                new System.ServiceModel.InstanceContext(cb);
            this._client = new OnlineLearningCardService.LearningCardServiceClient(context);
            try
            {
                this._client.RegisterClient(GlobalProfile.Instance.ActiveProfile.Name);
            }
            catch (EndpointNotFoundException)
            {
                System.Windows.Forms.MessageBox.Show(Model.GlobalLanguage.Instance.GetDict()["CouldNotConnectToTheServer"],
                    Model.GlobalLanguage.Instance.GetDict()["None"],
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }

        }

        private void StartHost(ServiceHost host)
        {
            try
            {
                host.Open();
            }
            catch (AddressAlreadyInUseException)
            {
                System.Windows.Forms.MessageBox.Show(Model.GlobalLanguage.Instance.GetDict()["ServerIsAlreadyRunning"],
                    Model.GlobalLanguage.Instance.GetDict()["None"],
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }
    }
}



