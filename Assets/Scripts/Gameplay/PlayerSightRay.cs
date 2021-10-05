using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay {
    public class PlayerSightRay : MonoBehaviour, IPlayerSiteRay
    {
        [SerializeField] NetObject netObj;
        [SerializeField] ScriptableRayCaster rayCaster;

        [SerializeField] LayerMask playerLayer;
        [SerializeField] List<string> playerTags;
        [SerializeField] float playerSightLenght = 10;
        private bool playerInSite;
        private GameObject playerObj;
        private TheRay ray;

        private void Awake() {
            if(netObj == null)
                netObj = GetComponent<NetObject>();
        }

        private void Start() {
            if(DevBoy.yes || netObj.IsMine) {

            } else
                Destroy(this);

            ray = new TheRay(rayCaster, playerSightLenght, playerTags, playerLayer, true, Color.black);
        }

        private void Update() {
            ray.RayResults(ref playerInSite, ref playerObj);
        }


        public bool InSite() {
            return playerInSite;
        }

        public GameObject PlayerInSiteObj() {
            return playerObj;
        }
    }
}