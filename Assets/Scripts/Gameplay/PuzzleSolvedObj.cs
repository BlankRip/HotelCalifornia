using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class PuzzleSolvedObj : MonoBehaviour, IInteractable
    {
        [SerializeField] ScriptablePuzzleStatusTracker exitDoor;
        private Collider myCollider;

        private void Start() {
            myCollider = GetComponent<Collider>();
        }

        public void Interact() {
            exitDoor.tracker.OnePuzzleSolved();
            myCollider.enabled = false;
        }

        public void HideInteractInstruction() {

        }

        public void ShowInteractInstruction() {
            Debug.Log("InteractNow");
        }
    }
}