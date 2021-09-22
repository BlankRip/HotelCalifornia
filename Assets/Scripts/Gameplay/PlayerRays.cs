using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay {
    public class PlayerRays : MonoBehaviour, IInteractRay
    {
        [SerializeField] NetObject netObj;
        [SerializeField] ScriptableRayCaster rayCaster;

        [SerializeField] LayerMask interactLayers;
        [SerializeField] float interactRayLength = 5;
        private bool canInteract;
        private IInteractable interactInView;

        [SerializeField] LayerMask playerLayers;
        [SerializeField] float playerSightLenght = 10;
        private bool playerInSite;
        GameObject playerObj;

        private Camera camera;
        private void Start() {
            camera = Camera.main;
            if(DevBoy.yes || netObj.IsMine) {

            } else
                Destroy(this);
        }

        private void Awake() {
            if(netObj == null)
                netObj = GetComponent<NetObject>();
        }

        private void Update() {
            PlayerLineOfSiteRay();
            InteractionRay();
        }

        private void InteractionRay() {
            RaycastHit hitInfo = rayCaster.caster.CastRay(interactLayers, interactRayLength);
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
            Debug.DrawRay(camera.transform.position, camera.transform.forward * interactRayLength, debugColor);
        }

        private void PlayerLineOfSiteRay() {
            RaycastHit hitInfo = rayCaster.caster.CastRay(playerLayers, playerSightLenght);
            Color debugColor = Color.blue;
            if(hitInfo.collider != null) {
                debugColor = Color.green;
                if(playerObj == null) {
                    playerObj = hitInfo.collider.gameObject;
                    playerInSite = true;
                }
            } else {
                if(playerObj != null) {
                    playerObj = null;
                    playerInSite = false;
                }
            }
            Debug.DrawRay(camera.transform.position, camera.transform.forward * playerSightLenght, debugColor);
        }

        public bool CanInteract() {
            return canInteract;
        }

        public void Interact() {
            interactInView.Interact();
        }
    }
}