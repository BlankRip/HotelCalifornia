using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    public class MixerSlot : MonoBehaviour, IInteractable, IMixerSlot
    {
        [SerializeField] Transform placementPoint;
        [SerializeField] IPortion slotted;
        private IMixer myMixer;
        
        private Collider trigger;
        bool inside;

        private void Start() {
            myMixer = GetComponentInParent<IMixer>();
            trigger = GetComponent<Collider>();
        }

        public void Interact() {
            if(slotted != null && !myMixer.IsMixing()) {
                myMixer.RemovePortion(slotted, this);
                slotted.Pick();
                slotted = null;
                inside = true;
                trigger.isTrigger = true;
            }
        }

        public void HideInteractInstruction() {
            Debug.LogError("HIDE ME BITCH!");
        }

        public void ShowInteractInstruction() {
            Debug.LogError("SHOW YOURSELF!");
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Potion") && !inside && slotted == null) {
                slotted = other.GetComponent<IPortion>();
                myMixer.AddPotion(slotted, this);
                slotted.Drop(true);
                trigger.isTrigger = false;
                other.transform.position = placementPoint.position;
                other.transform.rotation = Quaternion.identity;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("Potion")) {
                inside = false;
            }
        }

        public void DestroyItemInSlot() {
            Destroy(slotted.GetGameObject());
            inside = false;
            slotted = null;
        }
    }
}