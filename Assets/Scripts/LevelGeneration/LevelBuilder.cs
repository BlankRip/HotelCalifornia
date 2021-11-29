using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen {
    public class LevelBuilder : MonoBehaviour, ILevelBuilder
    {
        public bool generateSeed;
        [SerializeField] ScriptableLevelSeed seeder;
        [SerializeField] ScriptableLevelBuilder builder;
        [SerializeField] BuilderData builderData;
        [SerializeField] BuildingStatus currentStatus;

        //[SerializeField] int giveSeed = -1;

        private BuildingStatus backup;
        private IRoom startRoom;
        
        private void Awake() {
            builder.levelBuilder = this;
        }

        // private void Update() {
        //     if(Input.GetKeyDown(KeyCode.U))
        //         StartLevelGen(generateSeed);
        // }

        public void RestartingGeneration(bool seedStatus) {
            if(!seedStatus)
                Debug.LogError("This should never happen as a level gen with a succesful seed will never have to restart");
            generateSeed = seedStatus;
            StartLevelGen(generateSeed);
        }

        public void StartLevelGen(bool genSeed) {
            generateSeed = genSeed;
            builderData.onFail += RestartLevelGen;
            if(currentStatus.allRoomTypes.Count != currentStatus.eachTypeSpawned.Count) {
                currentStatus.eachTypeSpawned = new List<int>();
                for (int i = 0; i < currentStatus.allRoomTypes.Count; i++)
                    currentStatus.eachTypeSpawned.Add(0);
            }
            backup = new BuildingStatus(currentStatus);
            if(genSeed)
                seeder.levelSeed.GenerateSeed();

            // if(giveSeed != -1) {
            //     seeder.levelSeed.SetSeed(giveSeed);
            // }
            

            seeder.levelSeed.Initilize();
            StartCoroutine(GenerateLevel());
        }

        private IEnumerator GenerateLevel() {
            WaitForSeconds longInterval = new WaitForSeconds(1);
            WaitForFixedUpdate interval = new WaitForFixedUpdate();

            yield return longInterval;
            SpawnStartRoom();

            yield return interval;
            BaseLayoutBuilder baseBuild = gameObject.AddComponent<BaseLayoutBuilder>();
            baseBuild.Initilize(startRoom, builderData, ref currentStatus, ref backup);
            IBuilder baseBuilder = baseBuild;
            baseBuilder.StartBuilder();
            yield return interval;
            while(baseBuilder.GetBuilderStatus()) {
                //?Debug.Log("<color=black>Level Building</color>");
                yield return interval;
            }
            Debug.Log("<color=yellow>Base Built</color>");
            int x = seeder.levelSeed.GetSeed();
            Debug.LogError($"<color=red>SEED IS: {x}</color>");

            PuzzleBuilder puzzleBuild = gameObject.AddComponent<PuzzleBuilder>();
            puzzleBuild.Initilize(ref builderData);
            IBuilder puzzleBuilder = puzzleBuild;
            yield return new WaitForSeconds(6f);
            puzzleBuild.StartBuilder();
            yield return interval;
            while(puzzleBuild.GetBuilderStatus()) {
                //?Debug.Log("<color=cyan>Puzzle Building</color>");
                yield return interval;
            }
            Debug.Log("<color=yellow>Puzzles Built</color>");
            Debug.Log("<color=green>Level Built</color>");
            seeder.levelSeed.SeedSuccesful();

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
                restartSafe = false;
                this.gameObject.SetActive(false);
                Invoke("RestartGap", 0.5f);
            }
        }

        private void RestartGap() {
            Debug.Log($"Retrying Gen");
            LevelBuilder builder = GameObject.Instantiate(builderData.levelGen, transform.position, transform.rotation).GetComponent<LevelBuilder>();
            builder.RestartingGeneration(generateSeed);
            Destroy(this.gameObject);
        }
    }
}