using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Maze {
    public class MazeTP : MonoBehaviour
    {
        [SerializeField] ScriptableMazeEntryRoom mazeEntryRoom;
        [SerializeField] bool inMaze;
        private List<Transform> outPositions;

        private void Start() {
            if(inMaze) {
                outPositions = new List<Transform>();
                outPositions.Add(mazeEntryRoom.manager.GetExitPoint());
            } else
                outPositions = mazeEntryRoom.manager.GetPlayerEntryPoints();
        }
    }
}