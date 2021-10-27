using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class DelusionalEffect : MonoBehaviour, IAbilityEffect

    {
        [SerializeField] GameplayEventCollection eventCollection;
        public void ApplyEffect() {
            Debug.LogError("Start Delusions here");
            eventCollection.twistVision.Invoke();
        }
        
        //TODO: Only For Testing
        private void Update() {
            if(Input.GetKeyDown(KeyCode.O))
                ApplyEffect();
            if(Input.GetKeyDown(KeyCode.P))
                ResetEffect();
        }

        public void ResetEffect() {
            Debug.LogError("Stope seeing Delusions");
            eventCollection.fixVision.Invoke();
        }
    }
}