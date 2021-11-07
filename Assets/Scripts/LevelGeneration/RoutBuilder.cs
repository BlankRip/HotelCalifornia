using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen {
    public class RoutBuilder: MonoBehaviour, IBuilder
    {
        private bool buildInProgress;
        private int iterations;
        private BuildingStatus currentBuildingData;
        private BuildingStatus backupBuilderData;
        private BuilderData builderData;
        private List<GameObject> availableRoomsObjs;
        private List<GameObject> availableBackUp;
        private IRoomPlacer roomPlacer;
        Coroutine coroutine;

        public void Initilize(int iterations, BuilderData builderData, ref BuildingStatus currentBuildingData, ref BuildingStatus backupBuilderData) {
            this.iterations = iterations;
            this.builderData = builderData;
            this.currentBuildingData = currentBuildingData;
            this.backupBuilderData = backupBuilderData;

            availableRoomsObjs = new List<GameObject>();
            builderData.GetAllUsableRooms(currentBuildingData, ref availableRoomsObjs);
            availableBackUp = new List<GameObject>(availableRoomsObjs);

            roomPlacer = new RoomPlacer(builderData, currentBuildingData);
        }

        public void StartBuilder() {
            buildInProgress = true;
            coroutine = StartCoroutine(Build());
        }

        private IEnumerator Build() {
            WaitForSeconds longInterval = new WaitForSeconds(1);
            WaitForFixedUpdate interval = new WaitForFixedUpdate();

            yield return longInterval;
            for (int i = 0; i < iterations; i++) {
                //?Debug.Log("<color=black>Rout Building</color>");
                int rand = Random.Range(0, 100);
                if(rand > 80) {
                    PlaceCorridor();
                    yield return interval;
                    PlaceCorridor();
                    yield return interval;
                } else {
                    PlaceCorridor();
                    yield return interval;
                }

                bool placed = roomPlacer.PlaceRoom(ref availableRoomsObjs);
                if(!placed) 
                    RestartRout();
                yield return interval;
            }
            buildInProgress = false;

            yield return longInterval;
            Destroy(this);
        }

        private void PlaceCorridor() {
            bool placed = roomPlacer.PlaceRoom(builderData.corridors);
            if(!placed) 
                RestartRout();
        }

        private void RestartRout() {
            StopCoroutine(coroutine);
            //!StopAllCoroutines();
            availableRoomsObjs = new List<GameObject>(availableBackUp);
            currentBuildingData.retries++;
            if(currentBuildingData.retries >= builderData.maxRetries) {
                builderData.OnFaile();
                return;
            }

            if(currentBuildingData.currentRoutRooms.Count > 0) {
                for (int i = 0; i < currentBuildingData.currentRoutRooms.Count; i++)
                    currentBuildingData.currentRoutRooms[i].SelfKill();
            }
            UpdateCurrentOnFailRout();

            //?Debug.Log($"<color=red>Restarting Rout with retry Id: {currentBuildingData.retries}</color>");
            coroutine = StartCoroutine(Build());
        }

        private void UpdateCurrentOnFailRout() {
            currentBuildingData.eachTypeSpawned = new List<int>(backupBuilderData.eachTypeSpawned);
            currentBuildingData.availableDoorways = new List<Transform>(backupBuilderData.availableDoorways);
            currentBuildingData.currentRoutRooms.Clear();
        }

        public bool GetBuilderStatus() {
            return buildInProgress;
        }
    }
}