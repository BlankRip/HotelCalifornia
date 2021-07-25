using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public class RoutBuilder: IBuilder
    {
        private bool buildInProgress;
        private bool failed;
        private int iterations;
        private ScriptableLevelSeed seeder;
        private BuildingStatus currentBuildingData;
        private BuildingStatus backupBuilderData;
        private BuilderData builderData;
        private List<GameObject> availableRoomsObjs;
        private RoomPlacer roomPlacer;

        public RoutBuilder(int iterations, ScriptableLevelSeed levelSeed, BuilderData builderData, ref BuildingStatus currentBuildingData, ref BuildingStatus backupBuilderData) {
            this.iterations = iterations;
            seeder = levelSeed;
            this.builderData = builderData;
            this.currentBuildingData = currentBuildingData;
            this.backupBuilderData = backupBuilderData;

            availableRoomsObjs = new List<GameObject>();
            builderData.GetAllUsableRooms(currentBuildingData, ref availableRoomsObjs);

            roomPlacer = new RoomPlacer(seeder, builderData, currentBuildingData);
        }

        public void StartBuilder() {
            buildInProgress = true;
            
        }

        private IEnumerator Build() {
            WaitForSeconds longInterval = new WaitForSeconds(1);
            WaitForFixedUpdate interval = new WaitForFixedUpdate();

            yield return longInterval;
            for (int i = 0; i < iterations; i++) {
                int rand = seeder.levelSeed.GetRandomBetween(0, 100);
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
        }

        private void PlaceCorridor() {
            bool placed = roomPlacer.PlaceRoom(builderData.corridors);
            if(!placed) 
                RestartRout();
        }

        
        private void UpdateAvailableDoors(List<Transform> updateWith) {
            currentBuildingData.availableDoorways.Clear();
            currentBuildingData.availableDoorways = new List<Transform>(updateWith);
        }

        private void RestartRout() {
            //^ StopCoroutine(c);
            currentBuildingData.retries++;
            if(currentBuildingData.retries >= builderData.maxRetries) {
                failed = true;
                return;
            }
            seeder.levelSeed.ClearCurrent();

            if(currentBuildingData.currentRoutRooms.Count > 0) {
                for (int i = 0; i < currentBuildingData.currentRoutRooms.Count; i++)
                    currentBuildingData.currentRoutRooms[i].SelfKill();
            }
            UpdateCurrentOnFailRout();

            //!Debug.Log($"Restarting Rout with Id: {routId}");
            // c = StartCoroutine(SpawnRooms(tracker.currentIterations));
        }

        private void UpdateCurrentOnFailRout() {
            currentBuildingData.eachTypeSpawned = backupBuilderData.eachTypeSpawned;
            currentBuildingData.availableDoorways = backupBuilderData.availableDoorways;
            currentBuildingData.currentRoutRooms.Clear();
        }

        public bool GetBuilderStatus() {
            return buildInProgress;
        }

        public bool HasFailed() {
            return failed;
        }
    }
}