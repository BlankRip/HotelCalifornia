using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen
{
    public class LevelSeed : MonoBehaviour, ILevelSeed
    {
        [SerializeField] ScriptableLevelSeed seed;
        private bool generateSeed;
        private List<SeedData> probableSeed;
        public List<SeedData> theSeed;
        public List<SeedData> copiedData;
        int seedIndex;
        private int previousCount;
        private int saftyIndex;
        private int inValidSafty;
        private SeedData valueAtSaftyIndex;
        private bool correct;

        private void Awake() {
            seed.levelSeed = this;
            inValidSafty = -1;
        }

        public void Initilize() {
            probableSeed = new List<SeedData>();
            theSeed = new List<SeedData>();
            seedIndex = 0;
            valueAtSaftyIndex = new SeedData(inValidSafty, SeedValueType.RoutPick);
        }

        public int GetRandomBetween(int min, int max, SeedValueType valueType) {
            int randomeValue = 0;
            if(generateSeed) {
                randomeValue = Random.Range(min, max);
                UpdateProbableSeed(new SeedData(randomeValue, valueType));
            } else {
                randomeValue = GetValueFromSeed(valueType);
            }

            return randomeValue;
        }

        private void UpdateProbableSeed(SeedData value) {
            if(probableSeed.Count == 0)
                valueAtSaftyIndex = value;
            probableSeed.Add(value);
        }

        private int GetValueFromSeed(SeedValueType valueType) {
            int valueToReturn = -1;
            for (int i = 0; i < theSeed.Count; i++) {
                if(theSeed[i].valueType == valueType) {
                    valueToReturn = theSeed[i].value;
                    theSeed.RemoveAt(i);
                    break;
                }
            }
            return valueToReturn;
        }

        public void UpdateSeed() {
            if(generateSeed) {
                for (int i = 0; i < probableSeed.Count; i++)
                    theSeed.Add(probableSeed[i]);
                valueAtSaftyIndex.value = inValidSafty;
                ClearCurrent();
            }
        }

        public void ClearCurrent() {
            probableSeed.Clear();
            if(valueAtSaftyIndex.value != inValidSafty)
                probableSeed.Add(valueAtSaftyIndex);
        }

        public void TurnOnGeneration() {
            generateSeed = true;
            Initilize();
        }

        public void TurnOffGeneration() {
            generateSeed = false;
            copiedData = new List<SeedData>(theSeed);
        }
    }

    [System.Serializable]
    public struct SeedData
    {
        public int value;
        public SeedValueType valueType;

        public SeedData (int value, SeedValueType valueType) {
            this.value = value;
            this.valueType = valueType;
        }
    }
}