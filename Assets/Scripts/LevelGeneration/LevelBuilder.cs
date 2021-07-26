using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] GameObject levelGen;
        [SerializeField] bool generateSeed;
        [SerializeField] ScriptableLevelSeed seeder;
        [SerializeField] BuilderData builderData;
        [SerializeField] BuildingStatus current;

        private BuildingStatus backup;
        private IRoom startRoom;

        private void Start() {
            builderData.onFail += RestartLevelGen;
            backup = new BuildingStatus(current);
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
            baseBuild.Initilize(seeder, startRoom, builderData, ref current, ref backup);

            IBuilder baseBuilder = baseBuild;
            baseBuilder.StartBuilder();
            yield return interval;
            while(baseBuilder.GetBuilderStatus()) {
                //?Debug.Log("Level Building");
                if(baseBuilder.HasFailed())
                    RestartLevelGen();
                else
                    yield return interval;
            }
            Debug.Log("Base Built");
        }

        private void SpawnStartRoom() {
            GameObject sapwned = GameObject.Instantiate(builderData.startRoomObj, transform.position, transform.rotation);
            sapwned.transform.parent = this.transform;
            startRoom = sapwned.GetComponent<IRoom>();
        }

        public void RestartLevelGen() {
            Debug.Log($"Retrying Gen");
            GameObject.Instantiate(levelGen, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}