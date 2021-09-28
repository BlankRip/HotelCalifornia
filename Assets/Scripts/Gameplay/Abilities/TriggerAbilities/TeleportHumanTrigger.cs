using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class TeleportHumanTrigger : TriggerAbilities
    {
        float duration = 5;
        int usesLeft = 1;
        private void Start() {
            Initilize("UltimateUi", duration, AbilityEffectType.Teleport, usesLeft, true);
        }
    }
}