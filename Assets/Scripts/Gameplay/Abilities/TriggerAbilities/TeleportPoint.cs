using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class TeleportPoint : MonoBehaviour
    {
        [SerializeField] List<Transform> localTeleportPoints;

        public Vector3 GetTeleportPoint() {
            int rand = Random.Range(0, localTeleportPoints.Count);
            return localTeleportPoints[rand].position;
        }
    }
}