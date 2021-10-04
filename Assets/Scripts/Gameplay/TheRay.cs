using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class TheRay
    {
        private ScriptableRayCaster rayCaster;
        private LayerMask mask;
        private List<string> tags;
        private float rayLength;

        bool debugRay;
        private Camera camera;
        private Color debugColor;

        public TheRay(ScriptableRayCaster caster, float rayLength, List<string> tags, LayerMask mask, bool debugRay, Color debugColor) {
            this.tags = tags;
            this.mask = mask;
            this.rayLength = rayLength;
            rayCaster = caster;

            this.debugRay = debugRay;
            this.debugColor = debugColor;
            if(debugRay)
                camera = Camera.main;
        }

        public void RayResults(ref bool availableStatus, ref GameObject hitObject) {
            RaycastHit hitInfo = rayCaster.caster.CastRay(mask, rayLength);
            Color rayColor = debugColor;
            if(hitInfo.collider != null) {
                if(tags.Contains(hitInfo.collider.tag)) {
                    rayColor = Color.green;
                    if(hitObject == null) {
                        hitObject = hitInfo.collider.gameObject;
                        availableStatus = true;
                    }
                }
                if(debugRay)
                    Debug.DrawRay(camera.transform.position, camera.transform.forward * rayLength, rayColor);
                return;
            }

            if(hitObject != null) {
                hitObject = null;
                availableStatus = false;
            }
            if(debugRay)
                Debug.DrawRay(camera.transform.position, camera.transform.forward * rayLength, rayColor);
        }
    }
}