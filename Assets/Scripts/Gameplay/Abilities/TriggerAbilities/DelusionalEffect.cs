using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class DelusionalEffect : MonoBehaviour, IAbilityEffect
    {
        public void ApplyEffect() {
            Debug.LogError("Start Delusions here");
        }

        public void ResetEffect() {
            Debug.LogError("Stope seeing Delusions");
        }
    }
}