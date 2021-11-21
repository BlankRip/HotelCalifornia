using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.LevelGen;

namespace Knotgames.Gameplay.Puzzle.XO {
    public class XOSolutionRoom : MonoBehaviour, IXOSolutionRoom, IPairPuzzleSetup
    {
        [SerializeField] GameplayEventCollection eventCollection;
        [SerializeField] GameObject solutionBoardObj;
        [SerializeField] List<Transform> boardSpots;
        [SerializeField] float changeSolutionIn = 40;
        private ISolutionBoard solutionBoard;
        [SerializeField] List<string> currentSolution;
        private IXOPuzzleRoom puzzleRoom;
        private float timer;
        private bool timerOn;

        private void Start() {
            GameObject obj = GameObject.Instantiate(solutionBoardObj);
            obj.transform.SetParent(transform);
            solutionBoard = obj.GetComponent<ISolutionBoard>();
            solutionBoard.SetUpBoard();
            SetUpSolution();
            if(!DevBoy.yes)
                eventCollection.gameStart.AddListener(OnStart);
            if(DevBoy.yes)
                timerOn = true;
        }
        
        private void OnStart() {
            timerOn = true;
        }

        private void OnDestroy() {
            if(!DevBoy.yes)
                eventCollection.gameStart.RemoveListener(OnStart);
        }

        private void Update() {
            if(timerOn) {
                timer += Time.deltaTime;
                if(timer >= changeSolutionIn)
                    SetUpSolution();
            }
        }

        private void SetUpSolution() {
            int rand = KnotRandom.theRand.Next(0, boardSpots.Count);
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