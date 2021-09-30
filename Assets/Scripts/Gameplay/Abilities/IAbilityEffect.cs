using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public enum AbilityEffectType {
        Nada, BlurEffect, HumanProtection, ClearTraps, Teleport, Delusional
    }
    public interface IAbilityEffect
    {
        void ApplyEffect();
        void ResetEffect();
    }

    public interface IAbilityEffectTrigger
    {
        void TriggerEffect(AbilityEffectType type, float duration, bool masterOnly, bool sendData);
        bool IsUnderEffect();
    }
}