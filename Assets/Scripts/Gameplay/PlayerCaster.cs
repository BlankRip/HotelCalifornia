using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay {
    public class PlayerCaster : MonoBehaviour, IInteractRay, IPlayerSiteRay, ISphereCaster
    {
        [Header("Interaction in sight ray")]
        [SerializeField] NetObject netObj;
        [SerializeField] ScriptableRayCaster rayCaster;

        [SerializeField] LayerMask interactLayers;
        [SerializeField] float interactRayLength = 5;
        private bool canInteract;
        private IInteractable interactInView;


        [Space][Header("Player in sight ray")]
        [SerializeField] bool usePlayerSightRay;
        [SerializeField] LayerMask opositionPlayerLayers;
        [SerializeField] LayerMask friendlyPlayerLayers;
        [SerializeField] float playerSightLenght = 10;
        private bool playerInSite;
        private GameObject playerObj;

        [Space][Header("Trap placement rays")]
        [SerializeField] bool usePlacementRays;
        [SerializeField] LayerMask groundLayers;
        [SerializeField] LayerMask interactPlacableLayers;
        private LayerMask currentTrapLayers;
        private RaycastHit trapRayHitInfo;


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
            InteractionRay();
            if(usePlayerSightRay)
                PlayerLineOfSiteRay();
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
            RaycastHit hitInfo = rayCaster.caster.CastRay(opositionPlayerLayers, playerSightLenght);
            Color debugColor = Color.blue;
            if(hitInfo.collider != null) {
                debugColor = Color.green;
                if(playerObj == null) {
                    playerObj = hitInfo.collider.gameObject;
                    playerInSite = true;
                    Debug.LogError("IN VIEW");
                }
            } else {
                if(playerObj != null) {
                    playerObj = null;
                    playerInSite = false;
                    Debug.LogError("OUTTA VIEW");
                }
            }
            Debug.DrawRay(camera.transform.position, camera.transform.forward * playerSightLenght, debugColor);
        }

        private void TrapRay() {
            RaycastHit hitInfo = rayCaster.caster.CastRay(currentTrapLayers, playerSightLenght);
            Color debugColor = Color.blue;
            if(hitInfo.collider != null) {
                debugColor = Color.green;
                if(playerObj == null) {
                    playerObj = hitInfo.collider.gameObject;
                    playerInSite = true;
                    Debug.LogError("IN VIEW");
                }
            } else {
                if(playerObj != null) {
                    playerObj = null;
                    playerInSite = false;
                    Debug.LogError("OUTTA VIEW");
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

        public bool InSite() {
            return playerInSite;
        }

        public GameObject PlayerInSiteObj() {
            return playerObj;
        }

        public List<GameObject> GetOppositPlayersInSphere(float radius) {
            return GetPlayersInSphere(radius, opositionPlayerLayers);
        }

        public List<GameObject> GetFriendlyPlayersInSphere(float radius) {
            return GetPlayersInSphere(radius, friendlyPlayerLayers);
        }

        private List<GameObject> GetPlayersInSphere(float radius, LayerMask mask) {
            List<GameObject> objList = new List<GameObject>();
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, mask);
            foreach (Collider col in colliders)
                objList.Add(col.gameObject);

            return objList;
        }
    }
}