using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay
{
    public class BlurTrigger : TriggerAbilities
    {
        private void Start()
        {
            myEffect = new BlurEffect(abilityDuration);
            abilityDuration = 5;
            usesLeft = 5;
            Initilize("PrimaryUi", abilityDuration, TriggerEffectState.Blur, usesLeft);
        }
    }
}