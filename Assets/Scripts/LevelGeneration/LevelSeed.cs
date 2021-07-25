using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen
{
    public class LevelSeed : MonoBehaviour, ILevelSeed
    {
        [SerializeField] ScriptableLevelSeed seed;
        private bool generateSeed;
        private List<int> probableSeed;
        private Queue<int> theSeed;

        private void Awake() {
            seed.levelSeed = this;
            probableSeed = new List<int>();
            theSeed = new Queue<int>();
        }

        public int GetRandomBetween(int min, int max) {
            int randomeValue = 0;
            if(generateSeed) {
                randomeValue = Random.Range(min, max);
                UpdateProbableSeed(randomeValue);
            } else {
                randomeValue = GetValueFromSeed();
            }

            return randomeValue;
        }

        private void UpdateProbableSeed(int value) {
            probableSeed.Add(value);
        }

        public void ClearCurrent() {
            probableSeed.Clear();
        }

        public void UpdateSeed() {
            for (int i = 0; i < probableSeed.Count; i++)
                theSeed.Enqueue(probableSeed[i]);
            ClearCurrent();
        }

        private int GetValueFromSeed() {
            return theSeed.Dequeue();
        }

        public void TurnOnGeneration() {
            generateSeed = true;
        }

        public void TurnOffGeneration() {
            generateSeed = false;
        }
    }
}