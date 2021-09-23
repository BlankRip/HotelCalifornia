using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen {
    [CreateAssetMenu()]
    public class ScriptableLevelBuilder : ScriptableObject
    {
        public ILevelBuilder levelBuilder;
    }
}