using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public class BaseLayoutBuilder: MonoBehaviour, IBuilder 
    {
        private bool buildInProcess;
        private bool failed;
        private IRoom startRoom;
        private List<Transform> availableEntryDoors;
        private ScriptableLevelSeed seeder;
        private BuildingStatus currentBuildingData;
        private BuildingStatus backupBuilderData;
        private int routId;
        BuilderData builderData;
        private int finalRoutId = 3;
        public void Initilize(ScriptableLevelSeed levelSeed, IRoom startRoom,
            BuilderData builderData, ref BuildingStatus currentBuildingData, ref BuildingStatus backupBuilderData) {
            seeder = levelSeed;
            this.startRoom = startRoom;
            availableEntryDoors = startRoom.GetDoorways();
            this.currentBuildingData = currentBuildingData;
            this.backupBuilderData = backupBuilderData;
            this.builderData = builderData;
            routId = 0;
        }

        public void StartBuilder() {
            buildInProcess = true;
            StartCoroutine(Build());
        }

        private IEnumerator Build() {
            WaitForSeconds longInterval = new WaitForSeconds(1);
            WaitForFixedUpdate interval = new WaitForFixedUpdate();

            yield return longInterval;
            while(routId != finalRoutId) {
                PickRout();
                UpdateBackupWithCurrent();
                RoutBuilder routBuild = gameObject.AddComponent<RoutBuilder>();
                if(routId == finalRoutId)
                    routBuild.Initilize(builderData.finalIteration, seeder, builderData, ref currentBuildingData, ref backupBuilderData);
                else
                    routBuild.Initilize(builderData.iterations, seeder, builderData, ref currentBuildingData, ref backupBuilderData);

                IBuilder routBuilder = routBuild;
                routBuilder.StartBuilder();
                while(routBuilder.GetBuilderStatus()) {
                    //?Debug.Log("Base Building");
                    if(routBuilder.HasFailed()) {
                        Debug.Log("ENT");
                        failed = true;
                    }
                    else
                        yield return interval;
                }
                UpdateCurrentOnSuccessfulRout();
            }
            buildInProcess = false;

            yield return longInterval;
            Destroy(this);
        }

        private void PickRout() {
            int rand = seeder.levelSeed.GetRandomBetween(0, availableEntryDoors.Count);

            currentBuildingData.availableDoorways.Add(availableEntryDoors[rand]);
            availableEntryDoors.RemoveAt(rand);
            routId++;
        }

        private void UpdateBackupWithCurrent() {
            backupBuilderData = new BuildingStatus(currentBuildingData);
        }

        private void UpdateCurrentOnSuccessfulRout() {
            switch (routId) {
                case 1:
                    builderData.availableSide1Rooms = new List<IRoom>(currentBuildingData.currentRoutRooms);
                    break;
                case 2:
                    builderData.availableSide2Rooms = new List<IRoom>(currentBuildingData.currentRoutRooms);
                    break;
                case 3:
                    builderData.availableSide3Rooms = new List<IRoom>(currentBuildingData.currentRoutRooms);
                    break;
            }
            seeder.levelSeed.UpdateSeed();
            currentBuildingData.currentRoutRooms.Clear();
            currentBuildingData.availableDoorways.Clear();
            currentBuildingData.retries = 0;
        }

        public bool GetBuilderStatus() {
            return buildInProcess;
        }

        public bool HasFailed() {
            return failed;
        }
    }
}
