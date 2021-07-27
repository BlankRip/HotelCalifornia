using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public class PuzzleBuilder: IBuilder
    {
        private BuilderData builderData;
        private bool buildingPuzzles;
        IPuzzlePlacer pairPlacer;
        IPuzzlePlacer singlePlacer;

        public PuzzleBuilder(ScriptableLevelSeed levelSeed, BuilderData builderData, ref BuildingStatus currentBuildStatus) {
            this.builderData = builderData;

            pairPlacer = new PairPuzzlePlacer(levelSeed, ref currentBuildStatus);
            singlePlacer = new SinglePuzzlePlacer(levelSeed, ref currentBuildStatus);
        }

        public void StartBuilder() {
            buildingPuzzles = true;
        }

        public bool GetBuilderStatus() {
            return buildingPuzzles;
        }

        IEnumerator Build() {
            WaitForSeconds longInterval = new WaitForSeconds(1);
            WaitForFixedUpdate interval = new WaitForFixedUpdate();

            yield return longInterval;
            for (int i = 0; i < builderData.puzzlePairs; i++) {
                bool paced = pairPlacer.Place();
                if(paced)
                    yield return interval;
                else
                    builderData.OnFaile();
            }

            yield return longInterval;
            for (int i = 0; i < builderData.singelPuzzles; i++) {
                singlePlacer.Place();
                yield return interval;
            }
        }
    }
}
