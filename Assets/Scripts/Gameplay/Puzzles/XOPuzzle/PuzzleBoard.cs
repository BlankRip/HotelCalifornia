using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.XO {
    public class PuzzleBoard : MonoBehaviour, IPuzzleBoard
    {
        [SerializeField] List<BoardPiece> pieces;
        [SerializeField] Vector2Int fillRange = new Vector2Int(2, 5);
        [SerializeField] List<string> solution;
        IXOSolutionRoom solutionRoom;

        private void Start() {
            int fillCount = Random.Range(fillRange.x, fillRange.y + 1);
            List<BoardPiece> piecesCopy = new List<BoardPiece>(pieces);
            for (int i = 0; i < fillCount; i++) {
                int rand = Random.Range(0, piecesCopy.Count);
                piecesCopy[rand].SetRandom();
                piecesCopy.RemoveAt(rand);
            }
        }

        public void SetUp(IXOSolutionRoom solutionRoom) {
            this.solutionRoom = solutionRoom;
            int fillCount = Random.Range(fillRange.x, fillRange.y) + 1;
            List<BoardPiece> piecesCopy = new List<BoardPiece>(pieces);
            for (int i = 0; i < fillCount; i++) {
                int rand = Random.Range(0, piecesCopy.Count);
                piecesCopy[rand].SetRandom();
                piecesCopy.RemoveAt(rand);
            }
        }

        public void SetSolution(List<string> sol) {
            solution = sol;
        }

        public void CheckSolution() {
            // for (int i = 0; i < pieces.Count; i++) {
            //     if(pieces[i].GetValue() != solution[i])
            //         return;
            // }

            //! here puzzle solved
            // solutionRoom.Solved();
        }
    }
}