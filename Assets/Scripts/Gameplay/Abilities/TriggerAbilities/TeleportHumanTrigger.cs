using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class TeleportHumanTrigger : TriggerAbilities
    {
        float duration = 1;
        int usesLeft = 2;
        private void Start() {
            Initilize("UltimateUi", duration, AbilityEffectType.Teleport, usesLeft, true);
        }
    }
}