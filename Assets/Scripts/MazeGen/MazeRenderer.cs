using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.LevelGen;

namespace Knotgames.MazeGen {
    public class MazeRenderer : MonoBehaviour
    {
        [SerializeField] ScriptableLevelSeed seeder;
        [SerializeField] uint width = 10;
        [SerializeField] uint hight = 10;
        [SerializeField] GameObject wall;
        [SerializeField] float wallSize = 1;
        [SerializeField] GameObject floor;
        [SerializeField] float wallHight;
        private WallState[,] maze;
        private float halfWallSize;
        private List<GameObject> floorTiles;

        public List<GameObject> CreateMazeAndGetFloorTiles() {
            maze = MazeGenerator.Generate(width, hight, seeder.levelSeed.GetSeed());
            halfWallSize = wallSize/2;
            wallHight = wallHight/2;

            floorTiles = new List<GameObject>();
            RenderMaze();
            
            return floorTiles;
        }

        private void RenderMaze() {
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < hight; j++) {
                    WallState cell = maze[i, j];
                    Vector3 pos = transform.position + new Vector3(-width/2 + (i * wallSize), 0, -hight/2 + (j * wallSize));

                    Transform floorPiece = GameObject.Instantiate(floor, transform).transform;
                    floorPiece.position = pos + new Vector3(0, -wallHight, 0);
                    floorTiles.Add(floorPiece.gameObject);

                    if(cell.HasFlag(WallState.Up)) {
                        Transform topWall = GameObject.Instantiate(wall, transform).transform;
                        topWall.position = pos + new Vector3(0, 0, halfWallSize);
                    }
                    if(cell.HasFlag(WallState.Left)) {
                        Transform leftWall = GameObject.Instantiate(wall, transform).transform;
                        leftWall.position = pos + new Vector3(-halfWallSize, 0, 0);
                        leftWall.eulerAngles = new Vector3(0, 90, 0);
                    }

                    if(j == 0) {
                        if(cell.HasFlag(WallState.Down)) {
                            Transform downWall = GameObject.Instantiate(wall, transform).transform;
                            downWall.position = pos + new Vector3(0, 0, -halfWallSize);
                        }
                    }
                    if(i == width - 1) {
                        if(cell.HasFlag(WallState.Right)) {
                            Transform rightWall = GameObject.Instantiate(wall, transform).transform;
                            rightWall.position = pos + new Vector3(halfWallSize, 0, 0);
                            rightWall.eulerAngles = new Vector3(0, 90, 0);
                        }
                    }
                }
            }
        }
    }
}