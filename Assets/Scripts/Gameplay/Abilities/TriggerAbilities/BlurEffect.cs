using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay
{
    public class BlurEffect : MonoBehaviour, IAbilityEffect
    {
        public void ApplyEffect() {
            Debug.LogError("PUT BLURE HERE");
        }

        public void ResetEffect() {
            Debug.LogError("RESETTING!!");
        }
    }
}