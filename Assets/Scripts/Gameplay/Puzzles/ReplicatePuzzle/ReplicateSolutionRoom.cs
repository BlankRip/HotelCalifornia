using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.LevelGen;

namespace Knotgames.Gameplay.Puzzle.Replicate
{
    public class ReplicateSolutionRoom : MonoBehaviour, IReplicateSolutionRoom, IPairPuzzleSetup
    {
        [SerializeField] List<GameObject> replicateSolutionObjs;
        private IReplicateSolution replicateSolution;
        [SerializeField] List<string> currentSolution;
        private IReplicatePuzzleRoom puzzleRoom;

        private void Start()
        {
            GameObject go = PickRandomSolutionObj();
            replicateSolution = GameObject.Instantiate(go).GetComponent<IReplicateSolution>();
            SetUpSolution();
        }

        public GameObject PickRandomSolutionObj()
        {
            return replicateSolutionObjs[Random.Range(0, replicateSolutionObjs.Count)];
        }

        private void SetUpSolution()
        {
            currentSolution = replicateSolution.BuildNewSolution(transform);
            if (puzzleRoom != null)
                puzzleRoom.SetSolution(currentSolution);
        }

        public void Solved()
        {
            Destroy(this);
        }

        public void Link(GameObject obj, bool initiator)
        {
            puzzleRoom = obj.GetComponent<IReplicatePuzzleRoom>();
            puzzleRoom.SetSolution(currentSolution);
            if (initiator)
                obj.GetComponent<IPairPuzzleSetup>().Link(this.gameObject, false);
        }
    }
}