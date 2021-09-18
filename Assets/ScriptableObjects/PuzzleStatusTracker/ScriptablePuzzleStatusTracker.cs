using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class ScriptablePuzzleStatusTracker : ScriptableObject
    {
        public IPuzzleStatusTracker tracker;
    }
}