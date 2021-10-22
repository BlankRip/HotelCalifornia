using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Knotgames.Gameplay {
    public interface IGhostMoveAdjustment
    {
        float GetSpeed();
        void AdjustSpeed(float speedMultiplier);
        void SetSpeed(float speed);
        void LockMovement(bool state);
        void KnockBack();
    }

    public interface IHumanMoveAdjustment {
        void SetOnNextTpEvent(UnityAction call);
    }

    public interface IMoveAdjustment
    {
        void MoveToPosition(Vector3 position);
    }
}