using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay
{
    public class TriggerAbilities : MonoBehaviour, IAbility
    {
        private IAbilityUi myUi;
        private float abilityDuration;
        private int usesLeft;
        private IPlayerSiteRay playerSiteRay;
        private IAbilityEffectTrigger trigger;
        private AbilityEffectType effectType;
        private bool masterOnlyTrigger;

        protected void Awake()
        {
            playerSiteRay = GetComponent<IPlayerSiteRay>();
        }

        protected void Initilize(string uiTag, float abilityDuration, AbilityEffectType stateToSet, int numberOfUses, bool masterOnlyTrigger)
        {
            myUi = GameObject.FindGameObjectWithTag(uiTag).GetComponent<IAbilityUi>();
            this.abilityDuration = abilityDuration;
            this.masterOnlyTrigger = masterOnlyTrigger;
            usesLeft = numberOfUses;
            effectType = stateToSet;
            myUi.UpdateObjectData(usesLeft);
        }


        public bool CanUse()
        {
            if (playerSiteRay.InSite() && usesLeft!=0) {
                trigger = playerSiteRay.PlayerInSiteObj().GetComponent<IAbilityEffectTrigger>();
                if(!trigger.IsUnderEffect())
                    return true;
                else {
                    Debug.LogError("Could not activate");
                    return false;
                }
            } else {
                Debug.LogError("Could not activate");
                return false;
            }
        }

        public void UseAbility() {
            usesLeft--;
            myUi.UpdateObjectData(usesLeft);
            trigger.TriggerEffect(effectType, abilityDuration, masterOnlyTrigger, true);
            Debug.Log("<color=red>USING TRIGGER ABILITY!</color>");
        }

        public void Destroy() {
            Destroy(this);
        }
    }
}