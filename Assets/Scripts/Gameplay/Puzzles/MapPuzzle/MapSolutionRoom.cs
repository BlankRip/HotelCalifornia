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
        [SerializeField] List<string> connections;

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

        public bool Solved()
        {
            Invoke("L8Kill", 1f);
            return CheckMySol();
        }

        void L8Kill()
        {
            Destroy(this);
        }

        public bool CheckMySol()
        {
            foreach (string s in mapSolution.GetConnectionValues())
            {
                if (!connections.Contains(s))
                    return false;
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

        public void ResyncSolution(List<string> connections)
        {
            this.connections = connections;
        }
    }
}