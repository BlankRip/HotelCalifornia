using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class ClearTrapsTrigger : SelfAbilityTrigger
    {
        private int usesLeft = 1;
        private float duration = 0.1f;
        private void Start() {
            Initilize("UltimateUi", AbilityEffectType.ClearTraps, duration, usesLeft, false);
        }
    }
}