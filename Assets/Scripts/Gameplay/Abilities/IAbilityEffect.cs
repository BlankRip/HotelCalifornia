using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public enum AbilityEffectType {
        Nada, TestOne
    }
    public interface IAbilityEffect
    {
        void ApplyEffect();
    }

    public interface IAbilityEffectTrigger
    {
        void TriggerEffect(AbilityEffectType type);
    }
}