using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public class PuzzleBuilder: MonoBehaviour, IBuilder
    {
        private ScriptableLevelSeed seeder;
        private BuilderData builderData;
        private BuildingStatus currentBuildStatus;
        private bool buildingPuzzles;
        IPuzzlePlacer pairPlacer;
        IPuzzlePlacer singlePlacer;

        public void Initilize(ScriptableLevelSeed levelSeed, BuilderData builderData, ref BuildingStatus currentBuildStatus) {
            seeder = levelSeed;
            this.builderData = builderData;
            this.currentBuildStatus = currentBuildStatus;

        }

        public void StartBuilder() {
            buildingPuzzles = true;
            StartCoroutine(Build());
        }

        public bool GetBuilderStatus() {
            return buildingPuzzles;
        }

        IEnumerator Build() {
            WaitForSeconds longInterval = new WaitForSeconds(1);
            WaitForFixedUpdate interval = new WaitForFixedUpdate();

            yield return longInterval;
            pairPlacer = new PairPuzzlePlacer(seeder, ref currentBuildStatus);
            for (int i = 0; i < builderData.puzzlePairs; i++) {
                Debug.Log("<color=cyan>Placing Pairs</color>");
                bool paced = pairPlacer.Place();
                if(paced)
                    yield return interval;
                else
                    builderData.OnFaile();
            }

            yield return longInterval;
            singlePlacer = new SinglePuzzlePlacer(seeder, ref currentBuildStatus);
            for (int i = 0; i < builderData.singelPuzzles; i++) {
                Debug.Log("<color=cyan>Placing Singles</color>");
                singlePlacer.Place();
                yield return interval;
            }
            buildingPuzzles = false;

            yield return longInterval;
            Destroy(this);
        }
    }
}
