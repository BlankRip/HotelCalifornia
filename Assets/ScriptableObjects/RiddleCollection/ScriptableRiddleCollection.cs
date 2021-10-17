using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Riddler {
    [CreateAssetMenu()]
    public class ScriptableRiddleCollection : ScriptableObject
    {
        public List<Riddle> allRiddles;
        private List<Riddle> availableRiddles;

        public void SetUpForNewRiddleSet() {
            availableRiddles = new List<Riddle>(allRiddles);
        }

        public Riddle GetRandomRiddle() {
            int rand = Random.Range(0, availableRiddles.Count);
            Riddle riddleToReturn = availableRiddles[rand];
            availableRiddles.RemoveAt(rand);
            return riddleToReturn;
        }
    }

    [System.Serializable]
    public class Riddle
    {
        public string riddle;
        public string answer;
    }
}