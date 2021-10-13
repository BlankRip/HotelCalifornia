using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay.Abilities {
    public class PuzzleInterfearRay : MonoBehaviour, IInterfearRay
    {
        [SerializeField] NetObject netObj;
        [SerializeField] ScriptableRayCaster rayCaster;

        [SerializeField] LayerMask validLayers;
        [SerializeField] List<string> interfearableTags;
        [SerializeField] float rayLength = 5;
        private bool canInterfear;
        private GameObject hitObj;
        private IInterfear itemInView;
        private TheRay ray;

        private void Awake() {
            if(netObj == null)
                netObj = GetComponent<NetObject>();
        }

        private void Start() {
            if(DevBoy.yes || netObj.IsMine) {

            } else
                Destroy(this);
            
            ray = new TheRay(rayCaster, rayLength, interfearableTags, validLayers, false, Color.blue);
        }

        private void Update() {
            ray.RayResults(ref canInterfear, ref hitObj, false);
            if(hitObj != null) {
                itemInView = hitObj.GetComponent<IInterfear>();
                if(itemInView == null)
                    canInterfear = false;
            }
            else if(itemInView != null)
                itemInView = null;
        }

        public IInterfear GetInterfear() {
            return itemInView;
        }

        public bool ObjectAvailable() {
            return canInterfear;
        }
    }
}