using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.MazeGen;

namespace Knotgames.Gameplay.Puzzle.Maze {
    public class MazeEntryRoom : MonoBehaviour
    {
        [SerializeField] string mazePointsTag;
        [SerializeField] GameObject mazeGenObj;
        [SerializeField] int numberOfEntryPoints = 7;
        [SerializeField] GameObject entryObj;
        [SerializeField] int piecesToCollect = 3;
        [SerializeField] List<GameObject> pieceObjects;
        private List<GameObject> mazeFloorTiles;
        private List<GameObject> currentPieces;

        private void Start() {
            SetUpMaze();
            SpawnPieces();
        }

        private void SetUpMaze() {
            List<Transform> mazeSawnPoints = GameObject.FindGameObjectWithTag(mazePointsTag).GetComponent<TransformListHolder>().GetList();
            int rand = Random.Range(0, mazeSawnPoints.Count);
            MazeRenderer maze = GameObject.Instantiate(mazeGenObj, mazeSawnPoints[rand].position, Quaternion.identity).GetComponent<MazeRenderer>();
            mazeFloorTiles = maze.CreateMazeAndGetFloorTiles();
        }

        private void SelectPlayerEntryPoints() {
            List<Transform> playerSpawnPoints = new List<Transform>(new Transform[numberOfEntryPoints]);
            List<int> availableIndex = new List<int>();
            for (int i = 0; i < numberOfEntryPoints; i++)
                availableIndex.Add(i);
            
            for (int i = 0; i < numberOfEntryPoints; i++) {
                int rand = Random.Range(0, availableIndex.Count);
                int randTile = Random.Range(0, mazeFloorTiles.Count);
                playerSpawnPoints.Insert(availableIndex[rand], mazeFloorTiles[randTile].transform.GetChild(0));
                availableIndex.RemoveAt(rand);
                mazeFloorTiles.RemoveAt(randTile);
            }
        }

        private void SpawnPieces() {
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
    }
}