using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class BanishGhostTrigger : TriggerAbilities
    {
        private float duration = 1;
        private float abilityRange = 5;
        private int usesLeft = 1;
        private ISphereCaster sphereCaster;
        private List<GameObject> objsInSphere;
        private List<IAbilityEffectTrigger> myTriggers;

        private void Start() {
            myTriggers = new List<IAbilityEffectTrigger>();
            sphereCaster = GetComponent<ISphereCaster>();
            Initilize("SecondaryUi", duration, AbilityEffectType.Teleport, usesLeft, true);
        }

        protected override bool CanUseOverride() {
            objsInSphere = sphereCaster.GetOppositPlayersInSphere(abilityRange);
            if(UsesLeft != 0 && objsInSphere.Count != 0) {
                myTriggers.Clear();
                foreach (GameObject obj in objsInSphere)
                    myTriggers.Add(obj.GetComponent<IAbilityEffectTrigger>());
                SetTriggers(myTriggers);
                return true;
            } else
                return false;
        }
    }
}