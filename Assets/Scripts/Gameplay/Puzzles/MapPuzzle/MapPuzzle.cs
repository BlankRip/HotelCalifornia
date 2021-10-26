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
        IMapSolutionRoom solutionRoom;

        public void SetUp(IMapSolutionRoom solutionRoom)
        {
            this.solutionRoom = solutionRoom;
        }

        public void SetSolution(List<bool> sol)
        {
            solution = sol;
            solutionRoom.ResyncSolution(GenerateConnections());
        }

        List<string> GenerateConnections()
        {
            int j = 0;
            List<string> conekshuns = new List<string>();
            List<string> pieceIDs = new List<string>();
            foreach (MapPiece p in pieces)
            {
                pieceIDs.Add(j.ToString());
                j++;
            }

            for (int i = 0; i < pieces.Count / 2; i++)
            {
                int a = Random.Range(0, pieceIDs.Count);
                string x = pieceIDs[a];
                x += "-";
                pieceIDs.RemoveAt(a);
                a = Random.Range(0, pieceIDs.Count);
                x += pieceIDs[a];
                pieceIDs.RemoveAt(a);
                conekshuns.Add(x);
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

            if (solutionRoom.Solved())
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