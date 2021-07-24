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
        private BuildingData currentBuildingData;
        private BuildingData backupBuilderData;

        public RoutBuilder(int iterations, ScriptableLevelSeed levelSeed, ref BuildingData currentBuildingData, ref BuildingData backupBuilderData) {
            this.iterations = iterations;
            seeder = levelSeed;
            this.currentBuildingData = currentBuildingData;
            this.backupBuilderData = backupBuilderData;
        }

        public void StartBuilder() {
            buildInProgress = true;
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
