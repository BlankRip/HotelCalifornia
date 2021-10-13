using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Audio;

namespace Knotgames.Gameplay.Puzzle.Morse {
    public class MorseRoom : MonoBehaviour, IMorseRoom
    {
        [SerializeField] ScriptableMorsePuzzle morsePuzzle;
        [SerializeField] ScriptableMorseCollection morseCollection;
        [SerializeField] List<ClipName> clipNames;
        [SerializeField] List<SolutionPad> solutionPads;

        private Dictionary<char, AudioData> solDictianary;
        [SerializeField] List<char> solution;
        private MorseSolutionBuilder solutionBuilder;

        private void Awake() {
            morsePuzzle.manager = this;
            solutionBuilder = new MorseSolutionBuilder(clipNames, 3);
            solution = solutionBuilder.BuildSolution();
            solDictianary = solutionBuilder.GetSolutionDictionary();
        }

        private void Start() {
            SetUpPads();
        }

        private void SetUpPads() {
            foreach(char item in solution) {
                ClipName cName = solDictianary[item].clipName;
                clipNames.Remove(cName);
                string padValue = $"{item} = {morseCollection.GetMoresString(cName)}";
                ActivateAPad(padValue);
            }

            for (int i = 0; i < 2; i++) {
                int rand = Random.Range(0, clipNames.Count);
                string padValue = $"{solutionBuilder.GetRandomChar()} = {morseCollection.GetMoresString(clipNames[rand])}";
                ActivateAPad(padValue);
            }
        }

        private void ActivateAPad(string value) {
            int padId = Random.Range(0, solutionPads.Count);
            solutionPads[padId].UpdateText(value);
            solutionPads.RemoveAt(padId);
        }

        public List<char> GetSolution() {
            return solution;
        }

        public void Solved() {
            Debug.Log("Solved");
        }
    }
}