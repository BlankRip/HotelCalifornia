using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.LevelLight {
    public class LightLeverRoom : MonoBehaviour
    {
        [SerializeField] GameObject lightObj;
        [SerializeField] List<Transform> lightSpawnPoints;
        private SpawnAtGivenPoints lightSpawner;

        private void Start() {
            
        }
    }
}