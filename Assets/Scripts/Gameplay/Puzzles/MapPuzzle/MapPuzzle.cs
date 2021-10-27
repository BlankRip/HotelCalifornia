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

        List<string> conekshunsDebug;
        List<string> GenerateConnections()
        {
            int j = 0;
            List<string> conekshuns = new List<string>();
            List<string> pieceIDs = new List<string>();
            foreach (MapPiece p in pieces)
            {
                pieceIDs.Add(j.ToString());
                p.Setuptext(j.ToString());
                j++;
            }

            List<MapPiece> copy = new List<MapPiece>(pieces);
            
            for (int i = 0; i < pieces.Count / 2; i++)
            {
                int a = Random.Range(0, pieceIDs.Count);
                string x = pieceIDs[a];
                Vector3 pos1 = copy[a].transform.position;
                MapPiece startpiece = copy[a];
                x += "-";
                pieceIDs.RemoveAt(a);
                copy.RemoveAt(a);
                a = Random.Range(0, pieceIDs.Count);
                x += pieceIDs[a];
                Vector3 pos2 = copy[a].transform.position;
                pieceIDs.RemoveAt(a);
                copy.RemoveAt(a);
                conekshuns.Add(x);
                startpiece.SetupLR(i);
                startpiece.lineRenderer.SetPositions(new Vector3[] { pos1, pos2 });

            }
            conekshunsDebug = conekshuns;
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