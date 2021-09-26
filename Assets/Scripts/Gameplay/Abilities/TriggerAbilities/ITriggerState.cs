using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay
{
    [System.Serializable]
    public enum TriggerEffectState
    {
        Nada, Blur
    }
    public interface ITriggerState
    {
        TriggerEffectState GetTriggerState();
        void SetTriggerState(TriggerEffectState effectState, float resetTime, bool sendData);
    }
}