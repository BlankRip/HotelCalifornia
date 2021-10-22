using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.Abilities;

namespace Knotgames.Gameplay.Puzzle.Maze {
    public class MazeTv : MonoBehaviour, IInterfear
    {
        [SerializeField] ScriptableMazeManager maze;
        [SerializeField] string poolTag;
        [SerializeField] Transform objPosition;
        private bool fakeStatic;
        private float fakeTime = 15;
        private float timer;

        private void Start() {
            ObjectPool.instance.SpawnPoolObj(poolTag, objPosition.position, objPosition.rotation);
        }

        private void Update() {
            if(fakeStatic) {
                timer += Time.deltaTime;
                if(timer >= fakeTime) {
                    timer = 0;
                    fakeStatic = false;
                    maze.manager.SetStaticObjState(false);
                }
            }
        }

        public bool CanInterfear() {
            return !fakeStatic;
        }

        public void Interfear() {
            maze.manager.SetStaticObjState(true);
            fakeStatic = true;
        }
    }
}