using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] GameObject levelGen;
        [SerializeField] ScriptableLevelSeed seeder;
        [SerializeField] BuilderData builderData;
        [SerializeField] BuildingStatus current;

        private BuildingStatus backup;
        private IRoom startRoom;

        private void Start() {
            backup = new BuildingStatus(current);
            StartCoroutine(GenerateLevel());
        }

        private IEnumerator GenerateLevel() {
            WaitForSeconds longInterval = new WaitForSeconds(1);
            WaitForFixedUpdate interval = new WaitForFixedUpdate();

            yield return longInterval;
            SpawnStartRoom();
            yield return interval;
            IBuilder baseBuilder = new BaseLayoutBuilder(seeder, startRoom, builderData, ref current, ref backup);
            baseBuilder.StartBuilder();
            yield return interval;
            while(baseBuilder.GetBuilderStatus()) {
                if(baseBuilder.HasFailed())
                    RestartLevelGen();
                else
                    yield return interval;
            }
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