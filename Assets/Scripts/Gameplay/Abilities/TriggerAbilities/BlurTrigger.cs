using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities
{
    public class BlurTrigger : TriggerAbilities
    {
        float duration = 30;
        int usesLeft = 4;

        private void Start() {
            Initilize("SecondaryUi", duration, AbilityEffectType.BlurEffect, usesLeft, true);
        }
    }
}