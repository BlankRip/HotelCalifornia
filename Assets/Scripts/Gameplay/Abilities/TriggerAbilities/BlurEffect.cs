using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities
{
    public class BlurEffect : MonoBehaviour, IAbilityEffect
    {
        public void ApplyEffect() {
            Debug.LogError("PUT BLURE HERE");
            PlayerVissionBlur.instance.SetEffectState(State.active);
        }

        public void ResetEffect() {
            Debug.LogError("RESETTING!!");
            PlayerVissionBlur.instance.SetEffectState(State.inactive);
        }
    }
}