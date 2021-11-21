using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen {
    [CreateAssetMenu()]
    public class ScriptableLevelSeed : ScriptableObject
    {
        public ILevelSeed levelSeed;
        public static int fuckIndex;
        public static System.Random theRand;
    }
}
