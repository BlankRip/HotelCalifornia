using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    [CreateAssetMenu()]
    public class ScriptableLevelSeed : ScriptableObject
    {
        public ILevelSeed levelSeed;
    }
}
