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
        protected int UsesLeft {
            get{return usesLeft;}
        }
        private IPlayerSiteRay playerSiteRay;
        private List<IAbilityEffectTrigger> triggers;
        private AbilityEffectType effectType;
        private bool masterOnlyTrigger;

        protected void Awake()
        {
            playerSiteRay = GetComponent<IPlayerSiteRay>();
        }

        protected void Initilize(string uiTag, float abilityDuration, AbilityEffectType stateToSet, int numberOfUses, bool masterOnlyTrigger)
        {
            triggers = new List<IAbilityEffectTrigger>();
            myUi = GameObject.FindGameObjectWithTag(uiTag).GetComponent<IAbilityUi>();
            this.abilityDuration = abilityDuration;
            this.masterOnlyTrigger = masterOnlyTrigger;
            usesLeft = numberOfUses;
            effectType = stateToSet;
            myUi.UpdateObjectData(usesLeft);
        }


        public bool CanUse()
        {
            return CanUseOverride();
        }

        protected virtual bool CanUseOverride() {
            if (playerSiteRay.InSite() && usesLeft!=0) {
                triggers.Clear();
                triggers.Add(playerSiteRay.PlayerInSiteObj().GetComponent<IAbilityEffectTrigger>());
                if(!triggers[0].IsUnderEffect())
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

        protected void SetTriggers(List<IAbilityEffectTrigger> triggers) {
            this.triggers = triggers;
        }

        public void UseAbility() {
            usesLeft--;
            myUi.UpdateObjectData(usesLeft);
            foreach (IAbilityEffectTrigger trigger in triggers)
                trigger.TriggerEffect(effectType, abilityDuration, masterOnlyTrigger, true);
            Debug.Log("<color=red>USING TRIGGER ABILITY!</color>");
        }

        public void Destroy() {
            Destroy(this);
        }
    }
}