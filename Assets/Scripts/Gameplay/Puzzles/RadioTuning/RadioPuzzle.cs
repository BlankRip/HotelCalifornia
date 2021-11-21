using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.Radio
{
    public class RadioPuzzle : MonoBehaviour, IRadioTuner
    {
        [SerializeField] ScriptablePuzzleStatusTracker puzzleTracker;
        [SerializeField] List<string> solution;
        [SerializeField] List<TuningPiece> pieces;

        IRadioSolutionRoom solutionRoom;

        public void SetUp(IRadioSolutionRoom solutionRoom)
        {
            this.solutionRoom = solutionRoom;
            List<TuningPiece> piecesCopy = new List<TuningPiece>(pieces);
            for (int i = 0; i < 3; i++)
            {
                int rand = KnotRandom.theRand.Next(0, piecesCopy.Count);
                piecesCopy[rand].SetRandom();
                piecesCopy.RemoveAt(rand);
            }
        }

        public void SetSolution(List<string> sol)
        {
            solution = sol;
        }

        public void CheckSolution()
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                if (pieces[i].GetValue() != solution[i])
                    return;
            }

            solutionRoom.Solved();
            puzzleTracker.tracker.OnePuzzleSolved();
            Debug.Log("Solved");
            DestroyPuzzleOnComplete();
        }

        private void DestroyPuzzleOnComplete()
        {
            foreach (TuningPiece item in pieces)
            {
                item.gameObject.layer = 0;
                item.gameObject.tag = "Untagged";
                Destroy(item);
            }
            Destroy(this, 0.1f);
        }
    }
}