using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class NullAbilityEffect : MonoBehaviour, IAbilityResetter
    {
        private IGhostControlerAdjustment controlerAdjustment;

        private void Start() {
            controlerAdjustment = GetComponent<IGhostControlerAdjustment>();
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("RoomTrigger")) {
                if(other.GetComponent<IRoomState>().GetRoomState() == RoomEffectState.NoAbility) {
                    Debug.Log("Null Null");
                    controlerAdjustment.SetAbilityUsability(false);
                }
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.CompareTag("RoomTrigger")) {
                if(other.GetComponent<IRoomState>().GetRoomState() == RoomEffectState.NoAbility)
                    ResetEffect();
            }
        }

        public void ResetEffect() {
            Debug.Log("Null reset");
            controlerAdjustment.SetAbilityUsability(true);
        }
    }
}