using System.Collections;
using System.Collections.Generic;
using Knotgames.Audio;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities
{
    public class BlurEffect : MonoBehaviour, IAbilityEffect
    {
        public void ApplyEffect() {
            Debug.LogError("PUT BLURE HERE");
            AudioPlayer.instance.PlayAudio2DOneShot(ClipName.GhostWhisper);
            PlayerVissionBlur.instance.SetEffectState(State.active);
        }

        public void ResetEffect() {
            Debug.LogError("RESETTING!!");
            AudioPlayer.instance.Stop2DAudio();
            PlayerVissionBlur.instance.SetEffectState(State.inactive);
        }
    }
}