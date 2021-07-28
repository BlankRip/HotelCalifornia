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

        private int saftryIndex;
        private int saftyValue;
        public bool needSaftryUpdate = true;
        public void FillSafty(int value) {
            if(needSaftryUpdate) {
                saftryIndex = theSeed.Count;
                saftyValue = value;
                needSaftryUpdate = false;
            }
        }
        public void ApplySafty() {
            if(theSeed[saftryIndex] != saftyValue) {
                Debug.Log("<color=red>DirtyFix</color>");
                theSeed.Insert(saftryIndex, saftyValue);
            }
            needSaftryUpdate = true;
        }

        private void Awake() {
            seed.levelSeed = this;
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
                FillSafty(value);
            probableSeed.Add(value);
        }

        private int GetValueFromSeed() {
            int valueToReturn = theSeed[0];
            theSeed.RemoveAt(0);
            return valueToReturn;
        }

        public void UpdateSeed() {
            if(generateSeed) {
                for (int i = 0; i < probableSeed.Count; i++)
                    theSeed.Add(probableSeed[i]);
                ApplySafty();
                ClearCurrent();
            }
        }

        public void ClearCurrent() {
            probableSeed.Clear();
        }

        public void TurnOnGeneration() {
            generateSeed = true;
            Initilize();
        }

        private void Initilize() {
            probableSeed = new List<int>();
            theSeed = new List<int>();
        }

        public void TurnOffGeneration() {
            generateSeed = false;
        }
    }
}