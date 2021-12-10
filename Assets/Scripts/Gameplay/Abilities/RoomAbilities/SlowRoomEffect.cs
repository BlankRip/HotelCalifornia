using System.Collections;
using System.Collections.Generic;
using Knotgames.Audio;
using Knotgames.CharacterData;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class SlowRoomEffect : MonoBehaviour, IAbilityResetter
    {
        private IGhostMoveAdjustment moveAdjustment;
        private float normalSpeed;
        private float speedReductionMultiplier = 0.25f;
        [SerializeField] ScriptableCharacterSelect myCharData;

        private void Start() {
            moveAdjustment = GetComponent<IGhostMoveAdjustment>();
            normalSpeed = moveAdjustment.GetSpeed();
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("RoomTrigger")) {
                if(other.GetComponent<IRoomState>().GetRoomState() == RoomEffectState.Slow) {
                        moveAdjustment.AdjustSpeed(speedReductionMultiplier);
                        if(myCharData.characterType == CharacterType.Ghost)
                            AudioPlayer.instance.PlayAudio2DOneShot(ClipName.SlowDown);
                }
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