using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class GhostAbilityEffectTrigger : MonoBehaviour, IAbilityEffectTrigger
    {
        private bool underEffect;
        //^IAbilityEffect testEffect;
        private void Start() {
            //^Get components like testEffect = GetComponent<TestEffect>();
        }

        public void TriggerEffect(AbilityEffectType type, float duration) {
            switch(type) {
                case AbilityEffectType.Nada:
                    //^testEffect.ApplyEffect();
                break;
            }
        }

        public bool IsUnderEffect() {
            return underEffect;
        }
    }
}