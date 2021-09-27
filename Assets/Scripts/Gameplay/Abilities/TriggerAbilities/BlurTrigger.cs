using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay
{
    public class BlurTrigger : TriggerAbilities
    {
        float duration = 5;
        int usesLeft = 3;

        private void Start() {
            Initilize("PrimaryUi", duration, AbilityEffectType.BlurEffect, usesLeft, true);
        }
    }
}