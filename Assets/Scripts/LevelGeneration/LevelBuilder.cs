using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] bool generateSeed;
        [SerializeField] ScriptableLevelSeed seeder;
        [SerializeField] BuilderData builderData;
        [SerializeField] BuildingStatus currentStatus;

        private BuildingStatus backup;
        private IRoom startRoom;

        private void Start() {
            builderData.onFail += RestartLevelGen;
            backup = new BuildingStatus(currentStatus);
            if(generateSeed)
                seeder.levelSeed.TurnOnGeneration();
            else
                seeder.levelSeed.TurnOffGeneration();
            StartCoroutine(GenerateLevel());
        }

        private IEnumerator GenerateLevel() {
            WaitForSeconds longInterval = new WaitForSeconds(1);
            WaitForFixedUpdate interval = new WaitForFixedUpdate();

            yield return longInterval;
            SpawnStartRoom();

            yield return interval;
            BaseLayoutBuilder baseBuild = gameObject.AddComponent<BaseLayoutBuilder>();
            baseBuild.Initilize(seeder, startRoom, builderData, ref currentStatus, ref backup);
            IBuilder baseBuilder = baseBuild;
            baseBuilder.StartBuilder();
            yield return interval;
            while(baseBuilder.GetBuilderStatus()) {
                Debug.Log("<color=black>Level Building</color>");
                yield return interval;
            }
            Debug.Log("<color=yellow>Base Built</color>");

            PuzzleBuilder puzzleBuild = gameObject.AddComponent<PuzzleBuilder>();
            puzzleBuild.Initilize(seeder, builderData, ref currentStatus);
            IBuilder puzzleBuilder = puzzleBuild;
            puzzleBuild.StartBuilder();
            yield return interval;
            while(puzzleBuild.GetBuilderStatus()) {
                Debug.Log("<color=cyan>Puzzle Building</color>");
                yield return interval;
            }
            Debug.Log("<color=yellow>Puzzles Built</color>");
            Debug.Log("<color=green>Level Built</color>");

            yield return longInterval;
            Destroy(this);
        }

        private void SpawnStartRoom() {
            GameObject sapwned = GameObject.Instantiate(builderData.startRoomObj, transform.position, transform.rotation);
            sapwned.transform.parent = this.transform;
            startRoom = sapwned.GetComponent<IRoom>();
        }

        private bool restartSafe = true;
        public void RestartLevelGen() {
            if(restartSafe) {
                Debug.Log($"Retrying Gen");
                restartSafe = false;
                GameObject.Instantiate(builderData.levelGen, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }
}