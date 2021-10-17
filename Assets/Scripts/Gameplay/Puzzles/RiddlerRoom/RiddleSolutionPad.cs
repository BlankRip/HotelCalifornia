using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Riddler {
    public class RiddleSolutionPad : MonoBehaviour, IInteractable
    {
        [SerializeField] ScriptableRiddlerPuzzle thePuzzle;
        private RiddlerInputPanel inputPanel;
        private string mySolution;
        private bool solved;

        public void SetSolution(string solution, RiddlerInputPanel panel) {
            mySolution = solution.ToLower();
            inputPanel = panel;
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

        public void Interact() {
            if(!solved)
                inputPanel.OpenPanel(this);
        }

        public void ShowInteractInstruction() { }

        public void HideInteractInstruction() { }
    }
}