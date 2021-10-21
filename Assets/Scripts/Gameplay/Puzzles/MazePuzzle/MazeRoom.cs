using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.MazeGen;

namespace Knotgames.Gameplay.Puzzle.Maze {
    public class MazeRoom : MonoBehaviour, IMazeEntryRoom
    {
        [SerializeField] ScriptablePuzzleStatusTracker puzzleTracker;
        [SerializeField] GameplayEventCollection eventCollection;
        [SerializeField] ScriptableMazeRoom mazeRoom;
        [SerializeField] GameObject mazeGenObj;
        [SerializeField] int piecesToCollect = 3;
        [SerializeField] int numberOfEntryPoints = 7;
        [SerializeField] List<Transform> entryTpPoints;
        [SerializeField] GameObject entryTp;
        [SerializeField] GameObject exitTp;
        [SerializeField] Transform exitPoint;
        private IMazeManager mazeManager;
        private List<Transform> playerSpawnPoints;
        private float swapAfter = 70f;
        private float timer;
        private bool onTimer;

        private void Awake() {
            mazeRoom.manager = this;
            mazeManager = GameObject.Instantiate(mazeGenObj).GetComponent<IMazeManager>();
            mazeManager.SetUpMaze(exitTp);
            playerSpawnPoints = mazeManager.GetPlayerEntryPoints(numberOfEntryPoints);
            mazeManager.SpawnPieces(piecesToCollect);
            ActivateTPs();
            eventCollection.gameStart.AddListener(OnStart);
            if(DevBoy.yes)
                onTimer = true;
        }

        private void OnStart() {
            onTimer = true;
        }

        private void OnDestroy() {
            eventCollection.gameStart.RemoveListener(OnStart);
        }

        private void Update() {
            if(onTimer) {
                timer += Time.deltaTime;
                if(timer >= swapAfter) {
                    timer = 0;
                    mazeManager.SpawnPieces(piecesToCollect);
                }
            }
        }

        private void ActivateTPs() {
            int rand = Random.Range(0, entryTpPoints.Count);
            entryTp.transform.position = entryTpPoints[rand].position;
            entryTp.SetActive(true);
            exitTp.SetActive(true);
        }

        public List<Transform> GetPlayerEntryPoints() {
            return playerSpawnPoints;
        }

        public Transform GetExitPoint() {
            return exitPoint;
        }

        public void PieceCollected() {
            piecesToCollect--;
            if(piecesToCollect == 0) {
                Debug.Log("Solved");
                puzzleTracker.tracker.OnePuzzleSolved();
                onTimer = false;
            }
        }
    }
}