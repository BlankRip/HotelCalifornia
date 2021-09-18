using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class ExitDoor : MonoBehaviour, IPuzzleStatusTracker
    {
        [SerializeField] ScriptablePuzzleStatusTracker puzzleStatus;
        private int solvesNeeded = 3;
        private int puzzlesSolved;

        private void Awake() {
            puzzlesSolved = 0;
            puzzleStatus.tracker = this;
        }

        public void OnePuzzleSolved() {
            puzzlesSolved++;
            DoorVisualUpdate(puzzlesSolved);
            if(puzzlesSolved == solvesNeeded)
                OpenDoor();
        }

        private void DoorVisualUpdate(int solves) {
            //* Turn on ligts or somehting to indicate the number of puzzles solved
        }

        private void OpenDoor() {
            //* Open Door Here
        }
    }
}