using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen
{
    public class LevelSeed : MonoBehaviour, ILevelSeed
    {
        [SerializeField] ScriptableLevelSeed seed;

        private void Awake() {
            seed.levelSeed = this;
        }
        public int GetRandomBetween(int min, int max)
        {
            throw new System.NotImplementedException();
        }
    }
}