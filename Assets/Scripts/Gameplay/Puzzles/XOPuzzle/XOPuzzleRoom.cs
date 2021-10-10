using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.LevelGen;

namespace Knotgames.Gameplay.Puzzle.XO {
    public class XOPuzzleRoom : MonoBehaviour, IXOPuzzleRoom, IPairPuzzleSetup
    {
        [SerializeField] Transform puzzleBoard;
        [SerializeField] List<Transform> boardSpots;
        private IPuzzleBoard puzzle;

        public void Link(GameObject obj, bool initiator) {
            puzzle = puzzleBoard.GetComponent<IPuzzleBoard>();
            puzzle.SetUp(obj.GetComponent<IXOSolutionRoom>());
            if(initiator)
                obj.GetComponent<IPairPuzzleSetup>().Link(this.gameObject, false);
        }

        public void SetSolution(List<string> solution) {
            if(puzzle != null)
                puzzle.SetSolution(solution);
        }

        private void Awake() {
            int randSpot = Random.Range(0, boardSpots.Count);
            puzzleBoard.position = boardSpots[randSpot].position;
            puzzleBoard.rotation = boardSpots[randSpot].rotation;
        }
    }
}