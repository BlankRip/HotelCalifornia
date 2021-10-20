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
        List<string> sol;

        public void Link(GameObject obj, bool initiator)
        {
            Debug.LogError("LINKING REP PUZZLE");
            puzzle = replicatePuzzle.GetComponent<IReplicatePuzzle>();
            puzzle.SetUp(obj.GetComponent<IReplicateSolutionRoom>());
            puzzle.SetSolution(sol);
            if (initiator)
                obj.GetComponent<IPairPuzzleSetup>().Link(this.gameObject, false);
        }

        public void SetSolution(List<string> solution)
        {
            if (puzzle != null)
                puzzle.SetSolution(solution);

            sol = solution;
        }

        public void SpawnObjs(List<RepObj> objs)
        {
            Debug.LogError("SPAWNING REP OBJECTS!");
            foreach (RepObj o in objs)
            {
                Transform x = objSpawnAreas[Random.Range(0, objSpawnAreas.Count)];
                objSpawnAreas.Remove(x);
                Instantiate(o.Object, x.position, x.rotation, this.transform);
            }
        }
    }
}