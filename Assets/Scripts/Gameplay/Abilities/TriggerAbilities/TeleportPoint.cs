using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class TeleportPoint : MonoBehaviour
    {
        [SerializeField] List<Transform> localTeleportPoints;

        public Vector3 GetTeleportPoint() {
            int rand = KnotRandom.theRand.Next(0, localTeleportPoints.Count);
            return localTeleportPoints[rand].position;
        }
    }
}