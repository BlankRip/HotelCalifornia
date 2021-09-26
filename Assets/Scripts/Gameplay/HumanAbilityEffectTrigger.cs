using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class HumanAbilityEffectTrigger : MonoBehaviour, IAbilityEffectTrigger
    {
        //^IAbilityEffect testEffect;
        private bool underEffect;
        private float timer;
        private bool onTimer;
        private IAbilityEffect currentEffect;

        private IAbilityEffect blurEffect;


        private void Start() {
            blurEffect = GetComponent<BlurEffect>();
        }

        private void Update() {
            if(onTimer) {
                timer -= Time.deltaTime;
                if(timer <= 0) {
                    currentEffect.ResetEffect();
                    underEffect = false;
                    onTimer = false;
                    currentEffect = null;
                }
            }
        }

        public void TriggerEffect(AbilityEffectType type, float duration) {
            switch(type) {
                case AbilityEffectType.BlurEffect:
                    blurEffect.ApplyEffect();
                    currentEffect = blurEffect;
                    break;
            }

            underEffect = true;
            timer = duration;
            onTimer = true;
        }

        public bool IsUnderEffect() {
            return underEffect;
        }
    }
}