using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class PlayerNetData
    {
        public Knotgames.Network.DataSettings packetParam;
        public float horizontalInput;
        public float verticalInput;
        public bool moveYPositive;
        public bool moveYNegetive;

        public PlayerNetData() {
            packetParam = new Knotgames.Network.DataSettings("syncObjectData", "serveOthers");
            moveYPositive = false;
            moveYNegetive = false;
        }
    }

    public interface IPlayerMovement
    {
        void Move(float horizontalInput, float verticalInput, ref bool moveYPositive, ref bool moveYNegetive);
    }
}