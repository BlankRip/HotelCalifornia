using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class MoveTrapNutralizer : MonoBehaviour, IInteractable, ICancelableTrap
    {
        [SerializeField] ScriptableTrapTracker trapTracker;
        IMovementTrap movementTrap;

        private void Start() {
            movementTrap = GetComponentInParent<IMovementTrap>();
            trapTracker.tracker.AddToCancelable(this);
        }

        private void OnDestroy() {
            trapTracker.tracker.RemoveFromCancelable(this);
        }

        public void Interact() {
            movementTrap.DestroyTrap();
        }

        public void ShowInteractInstruction() {
            Debug.Log("Interact with left click to disable trap");
        }

        public void HideInteractInstruction()
        {
            Debug.Log("Hide instruction here");
        }

        public void Cancel() {
            movementTrap.DestroyTrap();
        }
    }
}