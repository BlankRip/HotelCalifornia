using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class SlowRoomEffect : MonoBehaviour, IAbilityResetter
    {
        private IGhostMoveAdjustment moveAdjustment;
        private float normalSpeed;
        private float speedReductionMultiplier = 0.25f;

        private void Start() {
            moveAdjustment = GetComponent<IGhostMoveAdjustment>();
            normalSpeed = moveAdjustment.GetSpeed();
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("RoomTrigger")) {
                if(other.GetComponent<IRoomState>().GetRoomState() == RoomEffectState.Slow)
                    moveAdjustment.AdjustSpeed(speedReductionMultiplier);
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.CompareTag("RoomTrigger")) {
                if(other.GetComponent<IRoomState>().GetRoomState() == RoomEffectState.Slow)
                    ResetEffect();
            }
        }

        public void ResetEffect() {
            moveAdjustment.SetSpeed(normalSpeed);
        }
    }
}