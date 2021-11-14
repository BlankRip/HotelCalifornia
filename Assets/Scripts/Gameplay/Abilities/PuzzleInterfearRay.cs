using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;
using Knotgames.Gameplay.UI;

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
                if(itemInView == null) {
                    InstructionText.instance.HideInstruction();
                    canInterfear = false;
                } else
                    InstructionText.instance.ShowInstruction("Press \'E\' To Try Interfere");
            }
            else if(itemInView != null) {
                InstructionText.instance.HideInstruction();
                itemInView = null;
            }
        }

        public IInterfear GetInterfear() {
            return itemInView;
        }

        public bool ObjectAvailable() {
            return canInterfear;
        }
    }
}