using System.Collections;
using System.Collections.Generic;
using Knotgames.Gameplay.UI;
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
            InstructionText.instance.ShowInstruction("Press \'LMB\' To Nutralize Trap");
        }

        public void HideInteractInstruction()
        {
            InstructionText.instance.HideInstruction();
        }

        public void Cancel() {
            movementTrap.DestroyTrap();
        }
    }
}