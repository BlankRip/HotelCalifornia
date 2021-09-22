using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay {
    public class CameraSetup : MonoBehaviour
    {
        [SerializeField] ScriptablePlayerCamera camera;
        [SerializeField] bool isGhost;
        [SerializeField] Transform camPos;
        [SerializeField] NetObject netObj;

        private void Start() {
            if(netObj == null)
                netObj = GetComponent<NetObject>();
            if(DevBoy.yes || netObj.IsMine)
                camera.cam.Initilize(this.transform, camPos, isGhost);
            Destroy(this, 0.2f);
        }
    }
}