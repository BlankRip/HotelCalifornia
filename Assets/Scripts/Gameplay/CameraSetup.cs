using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class CameraSetup : MonoBehaviour
    {
        [SerializeField] ScriptablePlayerCamera camera;
        [SerializeField] bool isGhost;
        [SerializeField] Transform camPos;

        private void Start() {
            camera.cam.Initilize(this.transform, camPos, isGhost);
            Destroy(this, 0.2f);
        }
    }
}