using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay.Abilities {
    public class SlowRoomTrigger : RoomAbilityTrigger
    {
        private float abilityDuration = 25;
        private int usesLeft = 1;

        private void Start() {
            Initilize("PrimaryUi", abilityDuration, RoomEffectState.Slow, usesLeft);
        }
    }
}