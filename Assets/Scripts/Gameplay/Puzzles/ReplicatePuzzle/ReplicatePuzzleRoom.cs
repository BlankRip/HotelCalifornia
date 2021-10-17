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
        List<RepObj> objsToSpawn;
        [SerializeField] List<Transform> objSpawnAreas;

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

        public void SetObjstoSpawn(List<RepObj> objs)
        {
            objsToSpawn = objs;
        }

        private void Start()
        {
            foreach(RepObj o in objsToSpawn)
            {
                Transform x = objSpawnAreas[Random.Range(0, objSpawnAreas.Count)];
                objSpawnAreas.Remove(x);
                Instantiate(o.Object, x.position, x.rotation, this.transform);
            }
            // replicatePuzzle.position = transform.position;
            // replicatePuzzle.rotation = transform.rotation;
        }
    }
}