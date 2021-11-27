using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay.Abilities {
    public class SelfProtectTrigger : SelfAbilityTrigger
    {
        private int usesLeft = 2;
        private float duration = 40;

        private void Start() {
            Initilize("PrimaryUi", AbilityEffectType.HumanProtection, duration, usesLeft, true);
        }
    }
}