using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    [CreateAssetMenu()]
    public class ScriptableTrapTracker : ScriptableObject
    {
        public ITrapTracker tracker;
    }
}