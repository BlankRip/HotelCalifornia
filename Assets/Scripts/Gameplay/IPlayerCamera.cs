using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public interface IPlayerCamera
    {
        void Initilize(Transform player, Transform camPos, bool ghost);
        void Lock(bool lockState);
        Transform GetTransform();
    }
}