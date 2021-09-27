using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay {
    public class SelfProtectTrigger : MonoBehaviour, IAbility
    {
        private IAbilityEffectTrigger effectTrigger;
        private IAbilityUi myUi;
        private int usesLeft = 2;
        private float duration;

        private void Start() {
            effectTrigger = GetComponent<IAbilityEffectTrigger>();
            myUi = GameObject.FindGameObjectWithTag("PrimaryUi").GetComponent<IAbilityUi>();
            myUi.UpdateObjectData(usesLeft);
        }

        public bool CanUse() {
            if(usesLeft != 0 && !effectTrigger.IsUnderEffect())
                return true;
            else {
                Debug.LogError("Cound not use Ability");
                return false;
            }
        }

        public void UseAbility() {
            usesLeft--;
            effectTrigger.TriggerEffect(AbilityEffectType.HumanProtection, duration, true);
        }

        public void Destroy() {
            Destroy(this);
        }
    }
}