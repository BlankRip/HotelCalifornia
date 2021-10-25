using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.LevelGen;

namespace Knotgames.Gameplay.Puzzle.Replicate
{
    public class ReplicateSolutionRoom : MonoBehaviour, IReplicateSolutionRoom, IPairPuzzleSetup
    {
        [SerializeField] GameObject replicateSolutionObj;
        private IReplicateSolution replicateSolution;
        [SerializeField] List<string> currentSolution;
        private IReplicatePuzzleRoom puzzleRoom;
        [SerializeField] List<Transform> objectSpots;

        private void Awake()
        {
            replicateSolution = GameObject.Instantiate(replicateSolutionObj, Vector3.zero, Quaternion.identity, transform).GetComponent<IReplicateSolution>();
            SetUpSolution();
        }

        private void SetUpSolution()
        {
            currentSolution = replicateSolution.BuildNewSolution(transform);
            if (puzzleRoom != null)
                puzzleRoom.SetSolution(currentSolution);
        }

        public void ResyncSolution(List<string> sol, List<RepObj> toSpawn)
        {
            currentSolution = sol;
            for (int i = 0; i < toSpawn.Count; i++)
            {
                int x = Random.Range(0, objectSpots.Count);
                RepObj repObj = toSpawn[i];
                GameObject go = Instantiate(repObj.Object, objectSpots[x].position, objectSpots[x].rotation, this.transform);
                go.layer = 0;
                repObj.SetOriginal(objectSpots[x].position, objectSpots[x].rotation);
                objectSpots.RemoveAt(x);
            }
        }

        public void Solved()
        {
            Destroy(this);
        }

        public void Link(GameObject obj, bool initiator)
        {
            Debug.LogError("LINKING REP SOLUTION");
            puzzleRoom = obj.GetComponent<IReplicatePuzzleRoom>();
            puzzleRoom.SetSolution(currentSolution);
            puzzleRoom.SpawnObjs(replicateSolution.GetStoredObjs());
            if (initiator)
                obj.GetComponent<IPairPuzzleSetup>().Link(this.gameObject, false);
        }

        public ReplicateObjectSlot GetCorrespondingSlot(int index)
        {
            return replicateSolution.GetCorrespondingSlot(index);
        }
    }
}