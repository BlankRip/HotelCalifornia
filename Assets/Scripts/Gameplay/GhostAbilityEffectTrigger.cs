using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class GhostAbilityEffectTrigger : MonoBehaviour, IAbilityEffectTrigger
    {
        //^IAbilityEffect testEffect;
        private void Start() {
            //^Get components like testEffect = GetComponent<TestEffect>();
        }

        public void TriggerEffect(AbilityEffectType type) {
            switch(type) {
                case AbilityEffectType.Nada:
                    //^testEffect.ApplyEffect();
                break;
            }
        }
    }
}