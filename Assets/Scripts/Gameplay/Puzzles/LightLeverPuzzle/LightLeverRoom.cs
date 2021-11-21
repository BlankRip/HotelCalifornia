using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.LevelGen;

namespace Knotgames.Gameplay.Puzzle.LeverLight {
    public class LightLeverRoom : MonoBehaviour, IPairPuzzleSetup
    {
        private static bool initilized = false;
        public static void Reset() {
            initilized = false;
        }
        [SerializeField] ScriptableLightLeverManager lightLever;
        [SerializeField] GameObject lightObj;
        [SerializeField] List<Transform> lightSpawnPoints;
        [SerializeField] GameObject leverObj;
        [SerializeField] List<Transform> leverSpawnPoints;
        [SerializeField] GameObject keyPadObj;
        [SerializeField] List<Transform> keyPadSpawnPoints;

        private IEnumerator Start() {
            if(!initilized)
                lightLever.Initilize();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            SpawnLevers();
            SpawnLights();
            if(!initilized)
                initilized = true;
            else
                SpawnKeyPad();
        }

        private void SpawnLights() {
            int amountToSpawn = lightLever.manager.GetLightsNeeded();
            if(!initilized)
                GetSpawnAmount(ref amountToSpawn);
            lightLever.manager.SubstractNeededLights(amountToSpawn);
            SpawnAtGivenPoints lightSpawner = new SpawnAtGivenPoints(lightSpawnPoints, lightObj, amountToSpawn, false);
            lightSpawner.Spawn(false, transform);
        }

        private void SpawnLevers() {
            SpawnAtGivenPoints leverSpawner = new SpawnAtGivenPoints(leverSpawnPoints, leverObj, 2, false);
            leverSpawner.Spawn(true, transform);
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

        private void SpawnKeyPad() {
            int rand = KnotRandom.theRand.Next(0, keyPadSpawnPoints.Count);
            GameObject.Instantiate(keyPadObj, keyPadSpawnPoints[rand].position, keyPadSpawnPoints[rand].rotation, transform);
        }

        public void Link(GameObject obj, bool initiator) {
            // Nothing to link for now
        }
    }
}