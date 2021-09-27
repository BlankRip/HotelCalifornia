using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public interface ITrapTracker 
    {
        void AddToCancelable(ICancelableTrap trap);
        void RemoveFromCancelable(ICancelableTrap trap);
        void CancelTraps();
    }

    public interface ICancelableTrap
    {
        void Cancel();
    }
}