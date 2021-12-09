using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

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
                bool needToSendData = !slotted.GetUseState();
                myMixer.AddPotion(slotted, this);
                slotted.SetMySlot(this);
                slotted.Drop();
                other.transform.position = placementPoint.position;
                other.transform.rotation = Quaternion.identity;

                if(!DevBoy.yes && needToSendData) 
                    slotted.GetGameObject().GetComponent<ILocalNetTransformSync>().SendDataOnRequest();
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