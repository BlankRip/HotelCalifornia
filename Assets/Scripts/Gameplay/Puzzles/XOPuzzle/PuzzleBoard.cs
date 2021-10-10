using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.XO {
    public class PuzzleBoard : MonoBehaviour, IPuzzleBoard
    {
        [SerializeField] ScriptablePuzzleStatusTracker puzzleTracker;
        [SerializeField] Transform boardForward;
        [SerializeField] string textObjPoolTag;
        [SerializeField] List<BoardPiece> pieces;
        [SerializeField] Vector2Int fillRange = new Vector2Int(2, 5);
        [SerializeField] List<string> solution;
        IXOSolutionRoom solutionRoom;

        public void SetUp(IXOSolutionRoom solutionRoom) {
            RotatePad();

            this.solutionRoom = solutionRoom;
            int fillCount = Random.Range(fillRange.x, fillRange.y) + 1;
            List<BoardPiece> piecesCopy = new List<BoardPiece>(pieces);
            for (int i = 0; i < fillCount; i++) {
                int rand = Random.Range(0, piecesCopy.Count);
                piecesCopy[rand].SetRandom();
                piecesCopy.RemoveAt(rand);
            }

            
            TextMeshProUGUI forwardText = ObjectPool.instance.SpawnPoolObj(textObjPoolTag, boardForward.position, boardForward.rotation).GetComponent<TextMeshProUGUI>();
            forwardText.text = "↑";
        }

        private void RotatePad() {
            int rotateDecider = Random.Range(0, 100);
            if(rotateDecider < 20)
                transform.Rotate(0, 0, 90);
            else if(rotateDecider > 50 && rotateDecider < 75)
                transform.Rotate(0, 0, -90);
            else if(rotateDecider < 90)
                transform.Rotate(0, 0, 180);
        }

        public void SetSolution(List<string> sol) {
            solution = sol;
        }

        public void CheckSolution() {
            for (int i = 0; i < pieces.Count; i++) {
                if(pieces[i].GetValue() != solution[i])
                    return;
            }

            solutionRoom.Solved();
            puzzleTracker.tracker.OnePuzzleSolved();
            Debug.Log("Solved");
            DestroyPuzzleOnComplete();
        }

        private void DestroyPuzzleOnComplete() {
            foreach (BoardPiece item in pieces) {
                item.gameObject.layer = 0;
                Destroy(item);
            }
            Destroy(this, 0.1f);
        }
    }
}