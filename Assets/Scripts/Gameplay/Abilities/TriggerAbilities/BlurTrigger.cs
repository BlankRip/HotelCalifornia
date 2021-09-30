using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities
{
    public class BlurTrigger : TriggerAbilities
    {
        float duration = 5;
        int usesLeft = 3;

        private void Start() {
            Initilize("SecondaryUi", duration, AbilityEffectType.BlurEffect, usesLeft, true);
        }
    }
}