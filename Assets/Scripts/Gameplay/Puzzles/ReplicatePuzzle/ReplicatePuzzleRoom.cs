using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.LevelGen;

namespace Knotgames.Gameplay.Puzzle.Replicate
{
    public class ReplicatePuzzleRoom : MonoBehaviour, IReplicatePuzzleRoom, IPairPuzzleSetup
    {
        [SerializeField] Transform replicatePuzzle;
        private IReplicatePuzzle puzzle;

        public void Link(GameObject obj, bool initiator)
        {
            puzzle = replicatePuzzle.GetComponent<IReplicatePuzzle>();
            puzzle.SetUp(obj.GetComponent<IReplicateSolutionRoom>());
            if (initiator)
                obj.GetComponent<IPairPuzzleSetup>().Link(this.gameObject, false);
        }

        public void SetSolution(List<string> solution)
        {
            if (puzzle != null)
                puzzle.SetSolution(solution);
        }

        private void Awake()
        {
            replicatePuzzle.position = transform.position;
            replicatePuzzle.rotation = transform.rotation;
        }
    }
}