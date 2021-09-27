using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class TrapTracker : MonoBehaviour, ITrapTracker
    {
        [SerializeField] ScriptableTrapTracker trapTracker;
        private List<ICancelableTrap> cancelableTraps;

        private void Awake() {
            trapTracker.tracker = this;
            cancelableTraps = new List<ICancelableTrap>();
        }

        public void AddToCancelable(ICancelableTrap trap) {
            if(!cancelableTraps.Contains(trap))
                cancelableTraps.Add(trap);
        }

        public void RemoveFromCancelable(ICancelableTrap trap) {
            if(cancelableTraps.Contains(trap))
                cancelableTraps.Remove(trap);
        }

        public void CancelTraps() {
            foreach (ICancelableTrap trap in cancelableTraps)
                trap.Cancel();
        }
    }
}