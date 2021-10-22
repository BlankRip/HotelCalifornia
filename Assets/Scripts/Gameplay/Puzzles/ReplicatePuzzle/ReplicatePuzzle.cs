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

        List<RepObj> toSpawn = new List<RepObj>();
        public void SetSolution(List<string> sol)
        {
            solution = sol;
            for(int i=0; i< slots.Count;i++)
            {
                if(solution[i] == "empty")
                {
                    RepObj repObj = GetRepObject();
                    GameObject go = Instantiate(repObj.Object, IncreasedHeightPos(slots[i].transform.position), slots[i].transform.rotation, this.transform);
                    go.layer = 0;
                    repObj.SetOriginal(IncreasedHeightPos(slots[i].transform.position), slots[i].transform.rotation);
                    solution[i] = repObj.name;
                    toSpawn.Add(repObj);
                    Destroy(slots[i]);
                    slots[i] = solutionRoom.GetCorrespondingSlot(i);
                }
            }
            solutionRoom.ResyncSolution(solution, toSpawn);
        }

        Vector3 IncreasedHeightPos(Vector3 pos)
        {
            return new Vector3(pos.x, pos.y + 5, pos.z);
        }

        private RepObj GetRepObject()
        {
            return replicateObjectDatabase.objects[Random.Range(0, replicateObjectDatabase.objects.Count)];
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
            foreach(ReplicateObjectSlot slot in slots)
            {
                slot.Disable();
            }
            Destroy(this, 0.1f);
        }
    }
}