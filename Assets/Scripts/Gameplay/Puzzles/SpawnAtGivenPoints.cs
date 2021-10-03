using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle {
    public class SpawnAtGivenPoints
    {
        private List<Transform> spawnPoints;
        private GameObject spawnObj;
        private int numberToSpawn;
        private bool useRandomRotation;

        public SpawnAtGivenPoints(List<Transform> spawnPoints, GameObject objToSpawn, int amount, bool useRandomRotation) {
            this.spawnObj = objToSpawn;
            this.spawnPoints = spawnPoints;
            numberToSpawn = amount;
            this.useRandomRotation = useRandomRotation;
        }

        public void Spawn() {
            for (int i = 0; i < numberToSpawn; i++) {
                int rand = Random.Range(0, spawnPoints.Count);
                GameObject spawned = GameObject.Instantiate(spawnObj, spawnPoints[rand].position, Quaternion.identity);
                if(useRandomRotation)
                    spawned.transform.up = (Random.insideUnitSphere).normalized;
                spawnPoints.RemoveAt(rand);
            }
        }
    }
}