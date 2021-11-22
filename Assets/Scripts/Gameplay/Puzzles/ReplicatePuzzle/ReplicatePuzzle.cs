using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.Replicate
{
    public class ReplicatePuzzle : MonoBehaviour, IReplicatePuzzle
    {
        [SerializeField] GameplayEventCollection eventCollection;
        [SerializeField] ReplicateObjectDatabase replicateObjectDatabase;
        [SerializeField] ScriptablePuzzleStatusTracker puzzleTracker;
        [SerializeField] List<string> solution;
        IReplicateSolutionRoom solutionRoom;
        [SerializeField] List<ReplicateObjectSlot> slots;
        List<RepObj> toSpawn = new List<RepObj>();
        List<GameObject> spawnedOnes = new List<GameObject>();

        public void SetUp(IReplicateSolutionRoom solutionRoom)
        {
            this.solutionRoom = solutionRoom;
        }

        private bool screwed;

        private void Start()
        {
            eventCollection.twistVision.AddListener(SwapObjects);
            eventCollection.fixVision.AddListener(ResetObjects);
        }

        public void SetSolution(List<string> sol)
        {
            solution = sol;
            for (int i = 0; i < slots.Count; i++)
            {
                if (solution[i] == "empty")
                {
                    RepObj repObj = GetRepObject();
                    GameObject go = Instantiate(repObj.Object, slots[i].transform.position, slots[i].transform.rotation, this.transform);
                    go.layer = 0;
                    Destroy(go.GetComponent<RepObjTransformSync>());
                    Destroy(go.GetComponent<ReplicateObject>());
                    Destroy(go.GetComponent<Rigidbody>());
                    repObj.SetOriginal(slots[i].transform.position, slots[i].transform.rotation);
                    solution[i] = repObj.name;
                    toSpawn.Add(repObj);
                    spawnedOnes.Add(go);
                    Destroy(slots[i]);
                    slots[i] = solutionRoom.GetCorrespondingSlot(i);
                    slots[i].SetPuzzle(this);
                }
            }
            solutionRoom.ResyncSolution(solution, toSpawn);
        }


        private RepObj GetRepObject()
        {
            return replicateObjectDatabase.objects[KnotRandom.theRand.Next(0, replicateObjectDatabase.objects.Count)];
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
            foreach (ReplicateObjectSlot slot in slots)
            {
                slot.Disable();
            }
            Destroy(this, 0.1f);
        }

        private void SwapObjects()
        {
            screwed = true;
            FlipObjects();
        }

        private void ResetObjects()
        {
            screwed = false;
            FlipObjects();
        }

        private void FlipObjects()
        {
            if (screwed)
            {
                for (int i = 0; i < spawnedOnes.Count; i++)
                {
                    MeshRenderer[] mrs = spawnedOnes[i].GetComponentsInChildren<MeshRenderer>();
                    foreach (MeshRenderer mr in mrs)
                    {
                        mr.enabled = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < spawnedOnes.Count; i++)
                {
                    MeshRenderer[] mrs = spawnedOnes[i].GetComponentsInChildren<MeshRenderer>();
                    foreach (MeshRenderer mr in mrs)
                    {
                        mr.enabled = true;
                    }
                }
            }
        }
    }
}