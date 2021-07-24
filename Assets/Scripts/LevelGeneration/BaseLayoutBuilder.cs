using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public class BaseLayoutBuilder: IBuilder 
    {
        private bool buildInProcess;
        private bool failed;
        private IRoom startRoom;
        private List<Transform> availableEntryDoors;
        private ScriptableLevelSeed seeder;
        private BuildingData currentBuildingData;
        private BuildingData backupBuilderData;
        private int routId;
        private int iterations;
        private int finalIteration;
        private int finalRoutId = 3;
        public BaseLayoutBuilder(ScriptableLevelSeed levelSeed, IRoom startRoom,
            int iterations, int finalIteration, ref BuildingData currentBuildingData, ref BuildingData backupBuilderData) {
            seeder = levelSeed;
            this.startRoom = startRoom;
            availableEntryDoors = startRoom.GetDoorways();
            this.currentBuildingData = currentBuildingData;
            this.backupBuilderData = backupBuilderData;
            this.iterations = iterations;
            this.finalIteration = finalIteration;
            routId = 0;
        }

        public void StartBuilder()
        {
            buildInProcess = true;
        }

        private IEnumerator Build() {
            WaitForSeconds longInterval = new WaitForSeconds(1);
            WaitForFixedUpdate interval = new WaitForFixedUpdate();

            yield return longInterval;
            //while(routId != finalRoutId) {
                PickRout();
                UpdateBackupWithCurrent();
                IBuilder routBuilder;
                if(routId == finalRoutId)
                    routBuilder = new RoutBuilder(finalIteration, seeder, ref currentBuildingData, ref backupBuilderData);
                else
                    routBuilder = new RoutBuilder(iterations, seeder, ref currentBuildingData, ref backupBuilderData);
                routBuilder.StartBuilder();
                while(routBuilder.GetBuilderStatus()) {
                    if(routBuilder.HasFailed())
                        failed = true;
                    else
                        yield return interval;
                }
                UpdateCurrentOnSuccessfulRout();
            //}
            buildInProcess = false;
        }

        private void PickRout() {
            int rand = seeder.levelSeed.GetRandomBetween(0, availableEntryDoors.Count);

            currentBuildingData.availableDoorways.Add(availableEntryDoors[rand]);
            availableEntryDoors.RemoveAt(rand);
            routId++;
        }

        private void UpdateBackupWithCurrent() {
            backupBuilderData = currentBuildingData;
        }

        private void UpdateCurrentOnSuccessfulRout() {
            switch (routId) {
                case 1:
                    currentBuildingData.availableSide1Rooms = new List<IRoom>(currentBuildingData.currentRoutRooms);
                    break;
                case 2:
                    currentBuildingData.availableSide2Rooms = new List<IRoom>(currentBuildingData.currentRoutRooms);
                    break;
                case 3:
                    currentBuildingData.availableSide3Rooms = new List<IRoom>(currentBuildingData.currentRoutRooms);
                    break;
            }
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
