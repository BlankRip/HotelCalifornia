using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class TestHumanInteract : MonoBehaviour, IInteractable
    {
        public void ShowInteractInstruction() {
            Debug.Log("Show instructions");
        }

        public void HideInteractInstruction() {
            Debug.Log("Hide instructions");
        }

        public void Interact() {
            Debug.Log("Interact with this stuff");
        }
    }
}