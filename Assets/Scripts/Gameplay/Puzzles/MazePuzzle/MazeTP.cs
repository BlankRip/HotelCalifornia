using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Maze {
    public class MazeTP : MonoBehaviour
    {
        [SerializeField] ScriptableMazeRoom mazeRoom;
        [SerializeField] bool inMaze;
        private List<Transform> outPositions;
        private int index = 0;
        private int lastIndex;

        private void Start() {
            if(inMaze) {
                outPositions = new List<Transform>();
                outPositions.Add(mazeRoom.manager.GetExitPoint());
            } else
                outPositions = mazeRoom.manager.GetPlayerEntryPoints();
            lastIndex = outPositions.Count - 1;
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Human")) {
                if(index == lastIndex)
                    index = 0;
                else
                    index++;
                other.GetComponent<IMoveAdjustment>().MoveToPosition(outPositions[index].position);
            }
        }
    }
}