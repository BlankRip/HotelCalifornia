using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay {
    public class PlayerSphearCaster : MonoBehaviour, ISphereCaster
    {
        [SerializeField] NetObject netObj;
        [SerializeField] ScriptableRayCaster rayCaster;
        
        [SerializeField] LayerMask playerLayer;
        [SerializeField] List<string> friendlyPlayerTags;
        [SerializeField] List<string> oppositPlayerTags;

        private void Awake() {
            if(netObj == null)
                netObj = GetComponent<NetObject>();
        }

        private void Start() {
            if(DevBoy.yes || netObj.IsMine) {

            } else
                Destroy(this);
        }

        public List<GameObject> GetOppositPlayersInSphere(float radius) {
            return GetPlayersInSphere(radius, oppositPlayerTags);
        }

        public List<GameObject> GetFriendlyPlayersInSphere(float radius) {
            return GetPlayersInSphere(radius, friendlyPlayerTags);
        }

        private List<GameObject> GetPlayersInSphere(float radius, List<string> tagMask) {
            List<GameObject> objList = new List<GameObject>();
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, playerLayer);
            foreach (Collider col in colliders) {
                if(tagMask.Contains(col.tag))
                    objList.Add(col.gameObject);
            }

            return objList;
        }
    }
}