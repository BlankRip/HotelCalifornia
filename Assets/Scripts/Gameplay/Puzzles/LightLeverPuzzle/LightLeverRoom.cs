using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.LevelGen;

namespace Knotgames.Gameplay.Puzzle.LevelLight {
    public class LightLeverRoom : MonoBehaviour, IPairPuzzleSetup
    {
        private static bool initilized = false;
        [SerializeField] ScriptableLightLeverManager lightLever;
        [SerializeField] GameObject lightObj;
        [SerializeField] List<Transform> lightSpawnPoints;
        [SerializeField] GameObject leverObj;
        [SerializeField] List<Transform> leverSpawnPoints;

        private void Start() {
            if(!initilized)
                lightLever.Initilize();
            SpawnPads();
            SpawnLights();
            if(!initilized)
                initilized = true;
        }

        private void SpawnLights() {
            int amountToSpawn = lightLever.manager.GetLightsNeeded();
            if(!initilized)
                GetSpawnAmount(ref amountToSpawn);
            lightLever.manager.SubstractNeedLights(amountToSpawn);
            SpawnAtGivenPoints lightSpawner = new SpawnAtGivenPoints(lightSpawnPoints, lightObj, amountToSpawn, false);
            lightSpawner.Spawn(false);
        }

        private void SpawnPads() {
            SpawnAtGivenPoints padSpawner = new SpawnAtGivenPoints(leverSpawnPoints, leverObj, 2, false);
            padSpawner.Spawn(true);
        }

        private void GetSpawnAmount(ref int totalAmount) {
            if(totalAmount == 36)
                totalAmount = 18;
            else if(totalAmount >= 31)
                totalAmount = 14;
            else if(totalAmount >= 26)
                totalAmount = 16;
            else if(totalAmount >= 16)
                totalAmount = 10;
            else
                totalAmount = 5;
        }

        public void Link(GameObject obj, bool initiator) {
            // Nothing to link for now
        }
    }
}