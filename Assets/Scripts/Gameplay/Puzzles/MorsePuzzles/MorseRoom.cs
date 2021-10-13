using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Audio;

namespace Knotgames.Gameplay.Puzzle.Morse {
    public class MorseRoom : MonoBehaviour
    {
        [SerializeField] ScriptableMorseCollection morseCollection;
        [SerializeField] List<ClipName> clipNames;
        [SerializeField] List<SolutionPad> solutionPads;
        Dictionary<char, AudioData> solDictianary;
        [SerializeField] List<char> solution;

        private void Start() {
            MorseSolutionBuilder solutionBuilder = new MorseSolutionBuilder(clipNames, 3);
            solution = solutionBuilder.BuildSolution();
            solDictianary = solutionBuilder.GetSolutionDictionary();
            SetUpPads();
        }

        private void SetUpPads() {
            foreach(char item in solution) {
                string padValue = $"{item} = {morseCollection.GetMoresString(solDictianary[item].clipName)}";
                int padId = Random.Range(0, solutionPads.Count);
                solutionPads[padId].UpdateText(padValue);
                solutionPads.RemoveAt(padId);
            }
        }
    }
}