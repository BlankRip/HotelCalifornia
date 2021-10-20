using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.Replicate
{
    public class ReplicatePuzzle : MonoBehaviour, IReplicatePuzzle
    {
        [SerializeField] ReplicateObjectDatabase replicateObjectDatabase;
        [SerializeField] ScriptablePuzzleStatusTracker puzzleTracker;
        [SerializeField] List<string> solution;
        IReplicateSolutionRoom solutionRoom;
        [SerializeField] List<ReplicateObjectSlot> slots;

        public void SetUp(IReplicateSolutionRoom solutionRoom)
        {
            this.solutionRoom = solutionRoom;
        }

        public void SetSolution(List<string> sol)
        {
            solution = sol;
        }

        public void CheckSolution()
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].GetValue() != solution[i])
                    return;
            }

            solutionRoom.Solved();
            puzzleTracker.tracker.OnePuzzleSolved();
            Debug.Log("Solved");
            DestroyPuzzleOnComplete();
        }

        private void DestroyPuzzleOnComplete()
        {
            Destroy(solutionRoom.GetMyGO(), 0.1f);
            foreach(ReplicateObjectSlot slot in slots)
            {
                Destroy(slot.gameObject);
            }
            Destroy(this.gameObject, 0.1f);
        }
    }
}