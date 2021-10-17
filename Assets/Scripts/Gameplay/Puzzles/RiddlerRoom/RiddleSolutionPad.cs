using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Riddler {
    public class RiddleSolutionPad : MonoBehaviour
    {
        [SerializeField] ScriptableRiddlerPuzzle thePuzzle;
        private string mySolution;
        private bool solved;

        public void SetSolution(string solution) {
            mySolution = solution.ToLower();
        }

        public void Check(string value) {
            CheckSolve(value);
        }

        private void CheckSolve(string value) {
            if(value.ToLower() == mySolution) {
                solved = true;
                thePuzzle.manager.UpdateSolve();
            } else {
                Debug.Log("Play Wrong ans sound");
            }
        }
    }
}