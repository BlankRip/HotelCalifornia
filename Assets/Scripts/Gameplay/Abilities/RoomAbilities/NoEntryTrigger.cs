using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class NoEntryTrigger : RoomAbilityTrigger
    {
        private float abilityDuration = 35;
        private int usesLeft = 1;

        private void Start() {
            Initilize("SecondaryUi", abilityDuration, RoomEffectState.NoEntry, usesLeft);
        }
    }
}