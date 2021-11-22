using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Audio;

namespace Knotgames.Gameplay.Puzzle.Morse {
    public class MorsePuzzle : MonoBehaviour, IMorsePuzzle
    {
        [SerializeField] ScriptableMorsePuzzle morsePuzzle;
        [SerializeField] ScriptableMorseCollection morseCollection;
        [SerializeField] List<ClipName> clipNames;
        [SerializeField] List<SolutionPad> solutionPads;
        [SerializeField] Transform device;
        [SerializeField] List<Transform> deviceSpots;
        [SerializeField] Transform player;
        [SerializeField] List<Transform> playerSpots;

        private Dictionary<char, AudioData> solDictianary;
        [SerializeField] List<char> solution;
        private MorseSolutionBuilder solutionBuilder;

        private void Awake() {
            morsePuzzle.manager = this;
            solutionBuilder = new MorseSolutionBuilder(clipNames, 3);
            solution = solutionBuilder.BuildSolution();
            solDictianary = solutionBuilder.GetSolutionDictionary();
            PositionDevices();
        }

        private void Start() {
            SetUpPads();
        }

        private void PositionDevices() {
            Transform spot = deviceSpots[KnotRandom.theRand.Next(0, deviceSpots.Count)];
            device.position = spot.position;
            device.rotation = spot.rotation;
            spot = playerSpots[KnotRandom.theRand.Next(0, playerSpots.Count)];
            player.position = spot.position;
            player.rotation = spot.rotation;
        }

        private void SetUpPads() {
            foreach(char item in solution) {
                ClipName cName = solDictianary[item].clipName;
                clipNames.Remove(cName);
                string padValue = $"{item} = {morseCollection.GetMorseString(cName)}";
                ActivateAPad(padValue);
            }

            for (int i = 0; i < 2; i++) {
                int rand = KnotRandom.theRand.Next(0, clipNames.Count);
                string padValue = $"{solutionBuilder.GetRandomChar()} = {morseCollection.GetMorseString(clipNames[rand])}";
                ActivateAPad(padValue);
            }
        }

        private void ActivateAPad(string value) {
            if(KnotRandom.theRand == null)
                KnotRandom.theRand = new System.Random(-1);
            int padId = KnotRandom.theRand.Next(0, solutionPads.Count);
            solutionPads[padId].UpdateText(value);
            solutionPads.RemoveAt(padId);
        }

        public List<char> GetSolution() {
            return solution;
        }

        public Dictionary<char, AudioData> GetSolutionDictionary() {
            return solDictianary;
        }
    }
}