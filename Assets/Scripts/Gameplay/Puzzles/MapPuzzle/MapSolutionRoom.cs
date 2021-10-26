using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.LevelGen;

namespace Knotgames.Gameplay.Puzzle.Map
{
    public class MapSolutionRoom : MonoBehaviour, IMapSolutionRoom, IPairPuzzleSetup
    {
        [SerializeField] GameObject mapSolutionObj;
        [SerializeField] List<Transform> mapSpots;
        private IMapSolution mapSolution;
        [SerializeField] List<bool> currentSolution;
        private IMapPuzzleRoom puzzleRoom;

        private void Start()
        {
            mapSolution = GameObject.Instantiate(mapSolutionObj).GetComponent<IMapSolution>();
            SetUpSolution();
        }

        private void SetUpSolution()
        {
            int rand = Random.Range(0, mapSpots.Count);
            currentSolution = mapSolution.BuildNewSolution(mapSpots[rand]);
            if (puzzleRoom != null)
                puzzleRoom.SetSolution(currentSolution);
        }

        public void Solved()
        {
            Destroy(this);
        }

        public void Link(GameObject obj, bool initiator)
        {
            puzzleRoom = obj.GetComponent<IMapPuzzleRoom>();
            puzzleRoom.SetSolution(currentSolution);
            if (initiator)
                obj.GetComponent<IPairPuzzleSetup>().Link(this.gameObject, false);
        }
    }
}