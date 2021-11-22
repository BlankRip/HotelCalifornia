using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.LevelGen;

namespace Knotgames.Gameplay.Puzzle.Map
{
    public class MapPuzzleRoom : MonoBehaviour, IMapPuzzleRoom, IPairPuzzleSetup
    {
        [SerializeField] Transform mapPuzzle;
        [SerializeField] List<Transform> mapSpots;
        private IMapPuzzle puzzle;

        public void Link(GameObject obj, bool initiator)
        {
            puzzle = mapPuzzle.GetComponent<IMapPuzzle>();
            puzzle.SetUp(obj.GetComponent<IMapSolutionRoom>());
            if (initiator)
                obj.GetComponent<IPairPuzzleSetup>().Link(this.gameObject, false);
        }

        public void SetSolution(List<bool> solution)
        {
            if (puzzle != null)
                puzzle.SetSolution(solution);
        }

        private void Awake()
        {
            int randSpot = KnotRandom.theRand.Next(0, mapSpots.Count);
            mapPuzzle.position = mapSpots[randSpot].position;
            mapPuzzle.rotation = mapSpots[randSpot].rotation;
        }
    }
}