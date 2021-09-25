using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public enum RoomEffectState {
        Nada, Slow, NoAbility, NoEntry
    }
    public interface IRoomState
    {
        void SetRoomState(RoomEffectState effectState, float resetTime);
        bool CanChangeState();
    }
}