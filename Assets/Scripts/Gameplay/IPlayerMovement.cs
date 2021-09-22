using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay {

    public interface IPlayerMovement
    {
        void Move(float horizontalInput, float verticalInput, ref bool moveYPositive, ref bool moveYNegetive);
    }
}