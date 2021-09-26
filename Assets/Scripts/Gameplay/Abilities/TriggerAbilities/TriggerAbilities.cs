using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay
{
    public class TriggerAbilities : MonoBehaviour, IAbility
    {
        private ITriggerState currentTrigger;
        protected IAbilityUi myUi;
        public float abilityDuration;
        public int usesLeft;
        protected PlayerRays playerRays;
        TriggerEffectState abilityState;
        protected Component myEffect;

        protected void Awake()
        {
            playerRays = GetComponent<PlayerRays>();
        }

        protected void Initilize(string uiTag, float abilityDuration, TriggerEffectState stateToSet, int numberOfUses)
        {
            myUi = GameObject.FindGameObjectWithTag(uiTag).GetComponent<IAbilityUi>();
            this.abilityDuration = abilityDuration;
            abilityState = stateToSet;
            usesLeft = numberOfUses;
            myUi.UpdateObjectData(usesLeft);
        }

        private void Update()
        {
            if (playerRays.PlayerInSight)
                currentTrigger = playerRays.ThePlayerInSight.GetComponent<ITriggerState>();
            else
                currentTrigger = null;
        }

        public bool CanUse()
        {
            if (playerRays.PlayerInSight && usesLeft != 0)
                return true;
            else
            {
                Debug.LogError("Could not activate");
                return false;
            }
        }

        public void UseAbility()
        {
            usesLeft--;
            myUi.UpdateObjectData(usesLeft);
            currentTrigger.SetTriggerState(abilityState, abilityDuration, true);
            Debug.Log("<color=red>USING TRIGGER ABILITY!</color>");
            SpawnEffect(playerRays.ThePlayerInSight);
        }

        protected void SpawnEffect(GameObject obj)
        {
            obj.AddComponent(myEffect.GetType());
        }

        public void Destroy()
        {
            Destroy(this);
        }
    }
}