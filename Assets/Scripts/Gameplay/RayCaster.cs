using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class RayCaster : MonoBehaviour, IRayCaster
    {
        [SerializeField] ScriptableRayCaster rayCaster;

        private void Awake() {
            rayCaster.caster = this;
        }

        public RaycastHit CastRay(LayerMask mask, float rayLength) {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit, rayLength, mask);
            
            return hit;
        }
    }
}