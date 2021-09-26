using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class NoEntryTrigger : RoomAbilityTrigger
    {
        private float abilityDuration = 40;
        private int usesLeft = 1;

        private void Start() {
            Initilize("SecondaryUi", abilityDuration, RoomEffectState.NoEntry, usesLeft);
        }
    }
}