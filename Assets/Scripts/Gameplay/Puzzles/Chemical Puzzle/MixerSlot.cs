using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    public class MixerSlot : MonoBehaviour, IMixerSlot
    {
        [SerializeField] Transform placementPoint;
        [SerializeField] IPortion slotted;
        private IMixer myMixer;

        private void Start() {
            myMixer = GetComponentInParent<IMixer>();
        }

        public void ReturingFromSlot() {
            myMixer.RemovePortion(slotted, this);
            Invoke("NullSlot", 0.03f);
        }

        private void NullSlot() {
            slotted = null;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Potion") && slotted == null) {
                slotted = other.GetComponent<IPortion>();
                myMixer.AddPotion(slotted, this);
                slotted.SetMySlot(this);
                slotted.Drop();
                other.transform.position = placementPoint.position;
                other.transform.rotation = Quaternion.identity;
            }
        }

        public void DestroyItemInSlot() {
            Destroy(slotted.GetGameObject());
            slotted = null;
        }

        public bool CanReturn() {
            return !myMixer.IsMixing();
        }
    }
}