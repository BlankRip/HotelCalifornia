using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class MoveTrapNutralizer : MonoBehaviour, IInteractable
    {
        IMovementTrap movementTrap;

        private void Start() {
            movementTrap = GetComponentInParent<IMovementTrap>();
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
    }
}