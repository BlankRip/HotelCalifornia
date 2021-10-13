using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Audio;

namespace Knotgames.Gameplay.Puzzle.Morse {
    public class MorseSolutionBuilder
    {
        private List<ClipName> clipNames;
        private int numberOfLetters;
        private List<char> myAlphas;
        private List<char> alphabets;
        Dictionary<char, AudioData> clipAlphabetDictionary;
        private List<char> solution;

        public MorseSolutionBuilder(List<ClipName> clipNames, int numberOfLetters) {
            this.clipNames = clipNames;
            this.numberOfLetters = numberOfLetters;
            SetUpSolutionRequirements();
        }

        private void SetUpSolutionRequirements() {
            myAlphas = new List<char>();
            alphabets = new List<char>{'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 
                'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
            solution = new List<char>();
            clipAlphabetDictionary = new Dictionary<char, AudioData>();
            solution = new List<char>();
        }

        public List<char> BuildSolution() {
            FillSoutionData();
            SetFinalSolution();
            return solution;
        }

        public Dictionary<char, AudioData> GetSolutionDictionary() {
            return clipAlphabetDictionary;
        }

        private void FillSoutionData() {
            for (int i = 0; i < numberOfLetters; i++)
            {
                char alpha = GetRandomChar();
                ClipName cName = clipNames[Random.Range(0, clipNames.Count)];
                clipNames.Remove(cName);

                clipAlphabetDictionary.Add(alpha, AudioPlayer.instance.GetClip(cName));
                myAlphas.Add(alpha);
            }
        }

        public char GetRandomChar() {
            char toReturn = alphabets[Random.Range(0, alphabets.Count)];
            alphabets.Remove(toReturn);
            return toReturn;
        }

        private void SetFinalSolution() {
            while (myAlphas.Count > 0)
            {
                int rand = Random.Range(0, myAlphas.Count);
                solution.Add(myAlphas[rand]);
                myAlphas.RemoveAt(rand);
            }
        }
    }
}