using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay {
    public class PlayerNetData
    {
        public string eventName;
        public string distributionOption;
        public string ownerID;
        public string objectID;
        public float horizontalInput;
        public float verticalInput;
        public bool moveYPositive;
        public bool moveYNegetive;

        public PlayerNetData(string netId = "") {
            eventName = "syncObjectData";
            distributionOption = "serveOthers";
            ownerID = NetConnector.instance.playerID.value;
            objectID = netId;
            moveYPositive = false;
            moveYNegetive = false;
        }
    }

    public interface IPlayerMovement
    {
        void Move(float horizontalInput, float verticalInput, ref bool moveYPositive, ref bool moveYNegetive);
    }
}