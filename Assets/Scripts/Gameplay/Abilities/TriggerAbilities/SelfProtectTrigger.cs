using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay.Abilities {
    public class SelfProtectTrigger : SelfAbilityTrigger
    {
        private int usesLeft = 1;
        private float duration = 60;

        private void Start() {
            Initilize("PrimaryUi", AbilityEffectType.HumanProtection, duration, usesLeft, true);
        }
    }
}