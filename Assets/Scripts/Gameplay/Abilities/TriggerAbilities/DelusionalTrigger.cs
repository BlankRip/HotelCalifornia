using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class DelusionalTrigger : TriggerAbilities
    {
        float duration = 30;
        int usesLeft = 2;

        private void Start() {
            Initilize("SecondaryUi", duration, AbilityEffectType.Delusional, usesLeft, true);
        }
    }
}