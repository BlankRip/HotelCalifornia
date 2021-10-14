using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.QuickDelivery {
    public class DeliveryItem : MonoBehaviour, IInteractable
    {
        private Transform attachPos;
        private bool held;

        private void Start() {
            attachPos = GameObject.FindGameObjectWithTag("AttachPos").transform;
        }

        public void Interact()
        {
            if (!held)
                Pick();
            else
                Drop();
        }

        public void Drop()
        {
            Debug.LogError("DROPPED");
            // if(!DevBoy.yes)
            //     transformSync.SetDataSyncStatus(false);
            // SendInUseData(false);

            held = false;
            transform.SetParent(null);
        }

        public void Pick()
        {
            // if(!DevBoy.yes)
            //     transformSync.SetDataSyncStatus(true);
            // SendInUseData(true);
            held = true;
            transform.SetParent(attachPos);
            transform.localPosition = Vector3.zero;
        }

        public void ShowInteractInstruction() { }

        public void HideInteractInstruction() { }
    }
}