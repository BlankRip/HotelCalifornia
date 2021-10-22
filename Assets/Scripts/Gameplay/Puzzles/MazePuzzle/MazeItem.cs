using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Maze {
    public class MazeItem : MonoBehaviour
    {
        [SerializeField] ScriptableMazeRoom mazeRoom;
        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Human")) {
                mazeRoom.manager.PieceCollected();
                Destroy(this.gameObject);
            }
        }
    }
}