using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen
{
    public class LevelSeed : MonoBehaviour, ILevelSeed
    {
        [SerializeField] ScriptableLevelSeed seed;
        int seedValue;

        private void Awake() {
            seed.levelSeed = this;
        }
        public void Initilize() {
            Random.InitState(seedValue);
        }

        // public int GetRandomBetween(int min, int max) {
        //     return Random.Range(min, max);
        // }

        public void GenerateSeed() {
            seedValue = Random.Range(int.MinValue, int.MaxValue);
        }
    }
}