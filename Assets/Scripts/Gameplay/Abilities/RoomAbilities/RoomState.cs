using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class RoomState : MonoBehaviour, IRoomState
    {
        private RoomEffectState roomState;
        private List<IAbilityResetter> resetters;
        private float resetIn;
        private bool onTimer;

        private void Start() {
            resetters = new List<IAbilityResetter>();
        }

        private void Update() {
            if(onTimer) {
                resetIn -= Time.deltaTime;
                if(resetIn <= 0) {
                    if(resetters.Count != 0) {
                        foreach (IAbilityResetter reset in resetters)
                            reset.ResetEffect();
                    }

                    roomState = RoomEffectState.Nada;
                    onTimer = false;
                }
            }
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player"))
                AddToResetters(other.gameObject);
        }

        private void AddToResetters(GameObject go) {
            IAbilityResetter[] currentResetters = go.GetComponents<IAbilityResetter>();
            if(currentResetters != null) {
                foreach (IAbilityResetter reset in currentResetters)
                    resetters.Add(reset);
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.CompareTag("Player"))
                RemoveFromResetters(other.gameObject);
        }

        private void RemoveFromResetters(GameObject go) {
            IAbilityResetter[] currentResetters = go.GetComponents<IAbilityResetter>();
            if(currentResetters != null) {
                foreach (IAbilityResetter reset in currentResetters) {
                    if(resetters.Contains(reset))
                        resetters.Remove(reset);
                }
            }
        }

        public RoomEffectState GetRoomState() {
            return roomState;
        }

        public bool CanChangeState() {
            if(resetters.Count == 0 && roomState == RoomEffectState.Nada)
                return true;
            else
                return false;
        }

        public void SetRoomState(RoomEffectState effectState, float resetTime) {
            roomState = effectState;
            resetIn = resetTime;
            onTimer = true;
        }
    }
}