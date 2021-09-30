using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class TrapRequirements : MonoBehaviour, ITrapRayRequirements
    {
        [SerializeField] ScriptableRayCaster rayCaster;
        [SerializeField] LayerMask groundLayers;
        [SerializeField] LayerMask placableLayers;
        [SerializeField] GameObject trapObj; //TODO Remove as this is just for testing

        public LayerMask GetGroundLayers() {
            return groundLayers;
        }

        public LayerMask GetPlacableLayers() {
            return placableLayers;
        }

        public ScriptableRayCaster GetRayCaster() {
            return rayCaster;
        }

        //TODO Remove as this is just for testing
        public GameObject GetTrapObj() {
            return trapObj;
        }
    }
}