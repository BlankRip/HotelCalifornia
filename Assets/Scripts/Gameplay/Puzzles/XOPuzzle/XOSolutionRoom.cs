using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.LevelGen;

namespace Knotgames.Gameplay.Puzzle.XO {
    public class XOSolutionRoom : MonoBehaviour, IXOSolutionRoom, IPairPuzzleSetup
    {
        [SerializeField] GameObject solutionBoardObj;
        [SerializeField] List<Transform> boardSpots;
        [SerializeField] float changeSolutionIn = 40;
        private ISolutionBoard solutionBoard;
        [SerializeField] List<string> currentSolution;
        private IXOPuzzleRoom puzzleRoom;
        private float timer;
        private bool timerOn = true;

        private void Start() {
            solutionBoard = GameObject.Instantiate(solutionBoardObj).GetComponent<ISolutionBoard>();
            solutionBoard.SetUpBoard();
            SetUpSolution();
        }

        private void Update() {
            if(timerOn) {
                timer += Time.deltaTime;
                if(timer >= changeSolutionIn)
                    SetUpSolution();
            }
        }

        private void SetUpSolution() {
            int rand = Random.Range(0, boardSpots.Count);
            currentSolution = solutionBoard.BuildNewSolution(boardSpots[rand]);
            timer = 0;

            if(puzzleRoom != null)
                puzzleRoom.SetSolution(currentSolution);
        }

        public void Solved() {
            Destroy(this);
        }

        public void Link(GameObject obj, bool initiator) {
            puzzleRoom = obj.GetComponent<IXOPuzzleRoom>();
            puzzleRoom.SetSolution(currentSolution);
            if(initiator)
                obj.GetComponent<IPairPuzzleSetup>().Link(this.gameObject, false);
        }
    }
}