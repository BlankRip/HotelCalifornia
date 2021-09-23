using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;

namespace Knotgames.Network {
    [System.Serializable]
    public class ObjectNetData {
        public string componentType;
    }

    [System.Serializable]
    public class EventNetData {
        public string eventName;
    }

    [System.Serializable]
    public class SeedPacket 
    {
        public int seed;
        public string eventName;
        public string roomID;
        public string distributionOption;

        public SeedPacket(int seed) {
            this.seed = seed;
            eventName = "seedGenerated";
            distributionOption = "serveOthers";
            if(!DevBoy.yes)
                roomID = NetRoomJoin.instance.roomID.value;
        }
    }

    [System.Serializable]
    public class PlayerNetData
    {
        public string eventName;
        public string componentType;
        public string distributionOption;
        public string ownerID;
        public string objectID;
        public string roomID;
        public float horizontalInput;
        public float verticalInput;
        public bool moveYPositive;
        public bool moveYNegetive;

        public PlayerNetData(string netId) {
            eventName = "syncObjectData";
            distributionOption = "serveOthers";
            componentType = "PlayerNetData";
            if(!DevBoy.yes)
            {
                ownerID = NetConnector.instance.playerID.value;
                roomID = NetRoomJoin.instance.roomID.value;
            }
            objectID = netId;
            moveYPositive = false;
            moveYNegetive = false;
            horizontalInput = 0;
            verticalInput = 0;
        }
    }

    [System.Serializable]
    public class ModelSpawnNetData
    {
        public string eventName;
        public string roomID;
        public string componentType;
        public string distributionOption;
        public string ownerID;
        public string objectID;
        public ModelType modelType;

        public ModelSpawnNetData(ModelType type, string netId) {
            eventName = "syncObjectData";
            distributionOption = "serveOthers";
            componentType = "ModelSpawnNetData";
            ownerID = NetConnector.instance.playerID.value;
            roomID = NetRoomJoin.instance.roomID.value;
            objectID = netId;
            modelType = type;
        }
    }

    [System.Serializable]
    public class SpawnObject
    {
        public string eventName;
        public string distributionOption;
        public string roomID;
        public string ownerID;
        public string objectName;
        public TransformWS transformWS;

        public SpawnObject(string objectName, Vector3 position, Quaternion rotation) {
            this.eventName = "spawnObject";
            this.distributionOption = "serveAll";
            this.roomID = NetRoomJoin.instance.roomID.value;
            this.ownerID = NetConnector.instance.playerID.value;
            this.objectName = objectName;
            transformWS = new TransformWS(position, rotation);
        }
    }
}