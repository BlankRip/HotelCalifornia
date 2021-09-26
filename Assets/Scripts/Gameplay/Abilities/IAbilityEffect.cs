using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public enum AbilityEffectType {
        Nada, BlurEffect
    }
    public interface IAbilityEffect
    {
        void ApplyEffect();
        void ResetEffect();
    }

    public interface IAbilityEffectTrigger
    {
        void TriggerEffect(AbilityEffectType type, float duration, bool sendData);
        bool IsUnderEffect();
    }
}