using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class ClearTrapsEffect : MonoBehaviour, IAbilityEffect
    {
        [SerializeField] ScriptableTrapTracker trapTracker;
        public void ApplyEffect() {
            trapTracker.tracker.CancelTraps();
        }

        public void ResetEffect() {
            
        }
    }
}