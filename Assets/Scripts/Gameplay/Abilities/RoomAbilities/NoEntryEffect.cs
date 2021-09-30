using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class NoEntryEffect : MonoBehaviour
    {
        private IGhostMoveAdjustment moveAdjustment;
        private void Start() {
            moveAdjustment = GetComponent<IGhostMoveAdjustment>();
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("RoomTrigger")) {
                if(other.GetComponent<IRoomState>().GetRoomState() == RoomEffectState.NoEntry)
                    moveAdjustment.KnockBack();
            }
        }
    }
}