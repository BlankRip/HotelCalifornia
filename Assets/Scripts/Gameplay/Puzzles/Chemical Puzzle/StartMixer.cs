using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    public class StartMixer : MonoBehaviour, IInteractable
    {
        private IMixer mixer;

        private void Start() {
            mixer = GetComponentInParent<IMixer>();
        }

        public void Interact() {
            mixer.StartMix();
        }

        public void HideInteractInstruction() {

        }

        public void ShowInteractInstruction() {

        }
    }
}