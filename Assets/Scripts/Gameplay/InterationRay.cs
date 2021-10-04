using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay {
    public class InterationRay : MonoBehaviour, IInteractRay
    {
        [SerializeField] NetObject netObj;
        [SerializeField] ScriptableRayCaster rayCaster;

        [SerializeField] LayerMask interactLayers;
        [SerializeField] List<string> interactableTags;
        [SerializeField] float interactRayLength = 5;
        private bool canInteract;
        private GameObject hitObj;
        private IInteractable interactInView;
        private TheRay ray;

        private void Awake() {
            if(netObj == null)
                netObj = GetComponent<NetObject>();
        }

        private void Start() {
            if(DevBoy.yes || netObj.IsMine) {

            } else
                Destroy(this);
            
            ray = new TheRay(rayCaster, interactRayLength, interactableTags, interactLayers, true, Color.blue);
        }

        private void Update() {
            ray.RayResults(ref canInteract, ref hitObj);
            if(hitObj != null)
                interactInView = hitObj.GetComponent<IInteractable>();
            else if(interactInView != null)
                interactInView = null;
        }

        public bool CanInteract() {
            return canInteract;
        }

        public void Interact() {
            interactInView.Interact();
        }
    }
}