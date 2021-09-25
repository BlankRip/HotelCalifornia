using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay {
    public class RoomAbilityTrigger: MonoBehaviour, IAbility
    {
        private IRoomState currentRoom;
        private IAbilityUi myUi;
        private float abilityDuration;
        private int usesLeft;
        RoomEffectState abilityState;
        
        protected void Initilize(string uiTag, float abilityDuration, RoomEffectState stateToSet, int numberOfUses) {
            myUi = GameObject.FindGameObjectWithTag(uiTag).GetComponent<IAbilityUi>();
            this.abilityDuration = abilityDuration;
            abilityState = stateToSet;
            usesLeft = numberOfUses;
            myUi.UpdateObjectData(usesLeft);
        }

        protected void OnTriggerEnter(Collider other) {
            if(other.CompareTag("RoomTrigger"))
                currentRoom = other.GetComponent<IRoomState>();
        }

        protected void OnTriggerExit(Collider other) {
            if(other.CompareTag("RoomTrigger"))
                currentRoom = null;
        }

        public bool CanUse() {
            if(usesLeft != 0 && currentRoom != null && currentRoom.CanChangeState())
                return true;
            else
                return false;
        }

        public void UseAbility() {
            usesLeft--;
            myUi.UpdateObjectData(usesLeft);
            currentRoom.SetRoomState(abilityState, abilityDuration);
        }

        public void Destroy() {
            Destroy(this);
        }
    }
}