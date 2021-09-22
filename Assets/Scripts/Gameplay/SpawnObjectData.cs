using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Network {
    public class SpawnObject
    {
        public DataSettings packetParam;
        public string objectName;
        public TransformWS transformWS;

        public SpawnObject(string objectName, Vector3 position, Quaternion rotation) {
            packetParam = new DataSettings("spawnObject", "serveAll");
            this.objectName = objectName;
            transformWS = new TransformWS(position, rotation);
        }
    }

    public class DataSettings
    {
        public string eventName;
        public string distributionOption;
        public string roomID;
        public string ownerID;

        public DataSettings(string eventName, string distributionOption) {
            this.eventName = eventName;
            this.distributionOption = distributionOption;
            this.roomID = NetRoomJoin.instance.roomID.value;
            this.ownerID = NetConnector.instance.playerID.value;
        }
    }
}

