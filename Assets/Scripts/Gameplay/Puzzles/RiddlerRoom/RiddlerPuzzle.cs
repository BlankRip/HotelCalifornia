using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Riddler {
    public class RiddlerPuzzle : MonoBehaviour, IRiddlerPuzzle
    {
        [SerializeField] ScriptableRiddlerPuzzle riddlerPuzzle;
        [SerializeField] ScriptablePuzzleStatusTracker puzzleTracker;
        [SerializeField] ScriptableRiddleCollection riddleCollection;
        [SerializeField] List<GameObject> riddleBoards;
        [SerializeField] int riddlesToSolve = 3;
        private int currentSolved;

        private void Awake() {
            riddlerPuzzle.manager = this;
        }

        private void Start() {
            riddleCollection.SetUpForNewRiddleSet();
            for (int i = 0; i < riddlesToSolve; i++) {
                int rand = KnotRandom.theRand.Next(0, riddleBoards.Count);
                riddleBoards[rand].SetActive(true);
                riddleBoards.RemoveAt(rand);
            }
            foreach(GameObject go in riddleBoards)
                Destroy(go);
        }

        public void UpdateSolve() {
            currentSolved++;
            if(currentSolved == riddlesToSolve)
                puzzleTracker.tracker.OnePuzzleSolved();
        }
    }
}