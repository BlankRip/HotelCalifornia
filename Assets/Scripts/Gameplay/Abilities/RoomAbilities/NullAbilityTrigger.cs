using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class NullAbilityTrigger : RoomAbilityTrigger
    {
        private float abilityDuration = 45;
        private int usesLeft = 1;

        private void Start() {
            Initilize("PrimaryUi", abilityDuration, RoomEffectState.NoAbility, usesLeft);
        }
    }
}