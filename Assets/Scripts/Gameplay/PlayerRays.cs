using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class PlayerRays : MonoBehaviour, IInteractRay
    {
        [SerializeField] ScriptableRayCaster rayCaster;
        [SerializeField] LayerMask interactLayers;
        [SerializeField] float rayLength = 5;
        private bool canInteract;
        private IInteractable interactInView;

        private Camera camera;
        private void Start() {
            camera = Camera.main;
        }

        private void Update() {
            InteractionRay();
        }

        private void InteractionRay() {
            RaycastHit hitInfo = rayCaster.caster.CastRay(interactLayers, rayLength);
            Color debugColor = Color.red;
            if(hitInfo.collider != null) {
                debugColor = Color.green;
                if(interactInView == null) {
                    interactInView = hitInfo.collider.gameObject.GetComponent<IInteractable>();
                    interactInView.ShowInteractInstruction();
                    canInteract = true;
                }
            } else {
                if(interactInView != null) {
                    interactInView.HideInteractInstruction();
                    interactInView = null;
                    canInteract = false;
                }
            }
            Debug.DrawRay(camera.transform.position, camera.transform.forward * rayLength, debugColor);
        }

        public bool CanInteract() {
            return canInteract;
        }

        public void Interact() {
            interactInView.Interact();
        }
    }
}