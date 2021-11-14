using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay.Abilities {
    public class SelfAbilityTrigger : MonoBehaviour, IAbility
    {
        private IAbilityEffectTrigger effectTrigger;
        private AbilityEffectType effectType;
        private IAbilityUi myUi;
        private int usesLeft;
        private float duration;
        private bool masterOnlyTrigger;

        private void Start() {
            myUi.UpdateObjectData(usesLeft);
        }

        protected void Initilize(string uiTag, AbilityEffectType effectType, float duration, int uses, bool masterOnlyTrigger) {
            effectTrigger = GetComponent<IAbilityEffectTrigger>();
            myUi = GameObject.FindGameObjectWithTag(uiTag).GetComponent<IAbilityUi>();
            this.effectType = effectType;
            this.usesLeft = uses;
            this.duration = duration;
            this.masterOnlyTrigger = masterOnlyTrigger;
        }

        public bool CanUse() {
            if(usesLeft != 0 && !effectTrigger.IsUnderEffect())
                return true;
            else {
                InstructionText.instance.ShowHideInstruction("Could not use Ability", 3);
                return false;
            }
        }

        public void UseAbility() {
            usesLeft--;
            myUi.UpdateObjectData(usesLeft);
            Debug.LogError(effectType);
            effectTrigger.TriggerEffect(effectType, duration, masterOnlyTrigger, true);
        }

        public void Destroy() {
            Destroy(this);
        }
    }
}