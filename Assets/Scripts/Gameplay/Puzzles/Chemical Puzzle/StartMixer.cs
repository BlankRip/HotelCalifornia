using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.Abilities;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    public class StartMixer : MonoBehaviour, IInteractable, IInterfear
    {
        private IMixer mixer;
        private bool useable;
        private float interfearDiableTime = 5f;

        private void Start() {
            mixer = GetComponentInParent<IMixer>();
            useable = true;
        }

        public void Interact() {
            if(useable)
                mixer.StartMix();
        }

        public void HideInteractInstruction() {

        }

        public void ShowInteractInstruction() {

        }

        public void Interfear() {
            useable = false;
            Invoke("MakeUsable", interfearDiableTime);
        }
        
        private void MakeUsable() {
            useable = true;
        }

        public bool CanInterfear() {
            return useable;
        }
    }
}