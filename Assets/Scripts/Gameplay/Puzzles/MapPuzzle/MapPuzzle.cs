using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.Map
{
    public class MapPuzzle : MonoBehaviour, IMapPuzzle
    {
        [SerializeField] ScriptablePuzzleStatusTracker puzzleTracker;
        [SerializeField] List<bool> solution;
        [SerializeField] List<MapPiece> pieces;
        [SerializeField] List<MapConnection> connections;
        IMapSolutionRoom solutionRoom;
        List<bool> conekshuns = new List<bool>();

        public void SetUp(IMapSolutionRoom solutionRoom)
        {
            this.solutionRoom = solutionRoom;
        }

        public void SetSolution(List<bool> sol)
        {
            solution = sol;
            solutionRoom.ResyncSolution(GenerateConnections());
        }
        List<bool> GenerateConnections()
        {
            foreach (MapConnection connection in connections)
            {
                connection.interactable = false;
                int rand = Random.Range(0, 2);
                if (rand == 0)
                    conekshuns.Add(false);
                else
                {
                    conekshuns.Add(true);
                    connection.TurnOn();
                }
            }
            return conekshuns;
        }

        public void CheckSolution()
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                if (pieces[i].GetValue() != solution[i])
                    return;
            }

            if (solutionRoom.CheckSolution())
            {
                puzzleTracker.tracker.OnePuzzleSolved();
                Debug.Log("Solved");
                DestroyPuzzleOnComplete();
            }
        }

        private void DestroyPuzzleOnComplete()
        {
            foreach (MapPiece item in pieces)
            {
                item.gameObject.layer = 0;
                item.gameObject.tag = "Untagged";
                Destroy(item);
            }
            Destroy(this, 0.1f);
        }
    }
}