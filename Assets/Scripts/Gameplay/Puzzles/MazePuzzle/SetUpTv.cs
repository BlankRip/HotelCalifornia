using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Maze {
    public class SetUpTv : MonoBehaviour
    {
        [SerializeField] string poolTag;
        [SerializeField] Transform objPosition;

        private void Start() {
            ObjectPool.instance.SpawnPoolObj(poolTag, objPosition.position, objPosition.rotation);
        }
    }
}