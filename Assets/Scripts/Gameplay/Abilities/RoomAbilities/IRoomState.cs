using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    [System.Serializable]
    public enum RoomEffectState {
        Nada, Slow, NoAbility, NoEntry
    }
    public interface IRoomState
    {
        RoomEffectState GetRoomState();
        void SetRoomState(RoomEffectState effectState, float resetTime, bool sendData);
        bool CanChangeState();
    }
}