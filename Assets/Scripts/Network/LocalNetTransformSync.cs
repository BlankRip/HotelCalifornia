using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Network {
    public class LocalNetTransformSync: MonoBehaviour, ILocalNetTransformSync
    {
        private LocalTransformSyncData dataToSend;
        private int myId;
        protected bool sendData;

        public void Initilize(string eventName, int id) {
            dataToSend = new LocalTransformSyncData(eventName, id);
            myId = id;
        }

        protected void RecieveData(string recieved) {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(recieved);
            if(extracted.myId == myId) {
                transform.position = extracted.position.ToVector();
                transform.rotation = extracted.rotation.ToQuaternion();
            }
        }

        protected void Update() {
            if(sendData)
                SendData();
        }

        public void SendData() {
            dataToSend.position = new PositionWS(transform.position);
            dataToSend.rotation = new RotationWS(transform.rotation);
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        public void SetDataSyncStatus(bool sync) {
            sendData = sync;
        }

        public virtual void SetID(int id) {
            
        }

        public void SendDataOnRequest() {
            SendData();
        }

        private class ExtractionClass
        {
            public int myId;
            public PositionWS position;
            public RotationWS rotation;
        }

        private class LocalTransformSyncData
        {
            public int myId;
            public PositionWS position;
            public RotationWS rotation;
            public string eventName;
            public string roomID;
            public string distributionOption;

            public LocalTransformSyncData(string syncEventName, int id) {
                eventName = syncEventName;
                distributionOption = DistributionOption.serveOthers;
                myId = id;
                if(!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }

}