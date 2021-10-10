using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.XO {
    public class XOSolutionRoom : MonoBehaviour, IXOSolutionRoom
    {
        [SerializeField] GameObject solutionBoardObj;
        [SerializeField] List<Transform> boardSpots;
        [SerializeField] float changeSolutionIn = 40;
        private ISolutionBoard solutionBoard;
        private List<string> currentSolution;
        private GameObject connection;
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

            if(connection != null) {
                //! update the soution in the other end
            }
        }

        public void Solved() {
            Destroy(this);
        }
    }
}