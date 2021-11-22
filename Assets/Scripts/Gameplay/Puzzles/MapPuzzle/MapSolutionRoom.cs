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
        [SerializeField] List<bool> connections;

        private void Start()
        {
            GameObject obj = GameObject.Instantiate(mapSolutionObj);
            mapSolution = obj.GetComponent<IMapSolution>();
            obj.transform.SetParent(transform);
            SetUpSolution();
        }

        private void SetUpSolution()
        {
            int rand = KnotRandom.theRand.Next(0, mapSpots.Count);
            currentSolution = mapSolution.BuildNewSolution(mapSpots[rand]);
            if (puzzleRoom != null)
                puzzleRoom.SetSolution(currentSolution);
        }

        public bool CheckSolution()
        {
            if (CheckMySol())
                Invoke("L8Kill", 1f);
            return CheckMySol();
        }

        void L8Kill()
        {
            mapSolution.Solved();
            Destroy(this);
        }

        public bool CheckMySol()
        {
            List<bool> connectionsToCheck = mapSolution.GetConnectionValues();
            int index = 0;
            foreach (bool toCheck in connectionsToCheck)
            {
                if (toCheck != connections[index])
                    return false;
                index++;
            }
            return true;
        }

        public void Link(GameObject obj, bool initiator)
        {
            puzzleRoom = obj.GetComponent<IMapPuzzleRoom>();
            puzzleRoom.SetSolution(currentSolution);
            if (initiator)
                obj.GetComponent<IPairPuzzleSetup>().Link(this.gameObject, false);
        }

        public void ResyncSolution(List<bool> connections)
        {
            this.connections = connections;
        }
    }
}