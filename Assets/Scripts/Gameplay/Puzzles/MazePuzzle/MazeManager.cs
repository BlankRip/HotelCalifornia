using System.Collections;
using System.Collections.Generic;
using Knotgames.MazeGen;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Maze {
    public class MazeManager : MonoBehaviour, IMazeManager
    {
        [SerializeField] ScriptableMazeManager maze;
        [SerializeField] string mazePointsTag;
        [SerializeField] List<GameObject> pieceObjects;
        [SerializeField] GameObject staticObj;
        private GameObject exitTp;
        private List<GameObject> mazeFloorTiles;
        private List<GameObject> currentPieces;
        private int humansInMaze;
        private bool forceStatic;

        private void Awake() {
            maze.manager = this;
        }

        public void SetStaticObjState(bool active) {
            if(active) {
                forceStatic = true;
                staticObj.SetActive(active);
            }
            else {
                forceStatic = false;
                if(humansInMaze > 0)
                    staticObj.SetActive(active);
            }
        }

        public void SetUpMaze(GameObject exitTp) {
            this.exitTp = exitTp;
            List<Transform> mazeSawnPoints = GameObject.FindGameObjectWithTag(mazePointsTag).GetComponent<TransformListHolder>().GetList();
            int rand = Random.Range(0, mazeSawnPoints.Count);
            transform.position = mazeSawnPoints[rand].position;
            MazeRenderer maze = GetComponent<MazeRenderer>();
            mazeFloorTiles = maze.CreateMazeAndGetFloorTiles();
        }

        public List<Transform> GetPlayerEntryPoints(int numberOfEntryPoints) {
            List<Transform> playerSpawnPoints = new List<Transform>(new Transform[numberOfEntryPoints]);
            List<int> availableIndex = new List<int>();
            for (int i = 0; i < numberOfEntryPoints; i++)
                availableIndex.Add(i);
            
            for (int i = 0; i < numberOfEntryPoints; i++) {
                int rand = Random.Range(0, availableIndex.Count);
                int randTile = Random.Range(0, mazeFloorTiles.Count);
                playerSpawnPoints[availableIndex[rand]] = mazeFloorTiles[randTile].transform.GetChild(0);
                availableIndex.RemoveAt(rand);
                mazeFloorTiles.RemoveAt(randTile);
            }
            return playerSpawnPoints;
        }

        public void SpawnPieces(int piecesToCollect) {
            ClearCurrentPieces();
            int pieceIndex = Random.Range(0, pieceObjects.Count);
            List<GameObject> copy = new List<GameObject>(mazeFloorTiles);
            int rand;
            Transform current;
            float minPieceGap = 13 * 13;

            for (int i = 0; i < piecesToCollect; i++) {
                rand = Random.Range(0, copy.Count);
                current = copy[rand].transform.GetChild(0);
                if(PassedRangeTest(current, minPieceGap)) {
                    GameObject spawned = GameObject.Instantiate(pieceObjects[pieceIndex], current.position, Quaternion.identity);
                    currentPieces.Add(spawned);
                } else
                    i--;
                copy.RemoveAt(rand);
            }
            rand = Random.Range(0, copy.Count);
            exitTp.transform.position = copy[rand].transform.GetChild(0).position;
        }

        private void ClearCurrentPieces() {
            if(currentPieces == null)
                currentPieces = new List<GameObject>();
            else {
                foreach(GameObject piece in currentPieces)
                    Destroy(piece);
                currentPieces.Clear();
            }
        }

        private bool PassedRangeTest(Transform curtrentPoint, float minDistance) {
            float distance;
            foreach(GameObject piece in currentPieces) {
                distance = (curtrentPoint.position - piece.transform.position).sqrMagnitude;
                if(distance >= minDistance)
                    continue;
                else
                    return false;
            }
            return true;
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Human")) {
                humansInMaze++;
                if(!forceStatic)
                    staticObj.SetActive(false);
                other.GetComponent<IHumanMoveAdjustment>().SetOnNextTpEvent(ExitingMaze);
            }
        }

        private void ExitingMaze() {
            humansInMaze--;
            if(humansInMaze == 0)
                staticObj.SetActive(true);
        }
    }
}