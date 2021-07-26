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
        private List<int> theSeed;
        int seedIndex;
        private int previousCount;
        private int saftyIndex;
        private int inValidSafty = -1;
        private int valueAtSaftyIndex;
        private bool correct;

        private void Awake() {
            seed.levelSeed = this;
            probableSeed = new List<int>();
            theSeed = new List<int>();
            seedIndex = 0;
            valueAtSaftyIndex = inValidSafty;
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
            if(probableSeed.Count == 0)
                valueAtSaftyIndex = value;
            probableSeed.Add(value);
        }

        private int GetValueFromSeed() {
            int valueToReturn = theSeed[0];
            theSeed.RemoveAt(0);
            return valueToReturn;
        }

        public void UpdateSeed() {
            for (int i = 0; i < probableSeed.Count; i++)
                theSeed.Add(probableSeed[i]);
            valueAtSaftyIndex = inValidSafty;
            ClearCurrent();
        }

        public void ClearCurrent() {
            probableSeed.Clear();
            if(valueAtSaftyIndex != inValidSafty)
                probableSeed.Add(valueAtSaftyIndex);
        }

        public void TurnOnGeneration() {
            generateSeed = true;
        }

        public void TurnOffGeneration() {
            generateSeed = false;
        }
    }
}