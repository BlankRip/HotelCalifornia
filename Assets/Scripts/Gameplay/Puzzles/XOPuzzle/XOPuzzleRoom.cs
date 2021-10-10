using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.XO {
    public class XOPuzzleRoom : MonoBehaviour
    {
        [SerializeField] Transform puzzleBoard;
        [SerializeField] List<Transform> boardSpots;
        private IPuzzleBoard puzzle;

        private void Awake() {
            int randSpot = Random.Range(0, boardSpots.Count);
            puzzleBoard.position = boardSpots[randSpot].position;
            puzzleBoard.rotation = boardSpots[randSpot].rotation;
            puzzle = puzzleBoard.GetComponent<IPuzzleBoard>();
        }
    }
}