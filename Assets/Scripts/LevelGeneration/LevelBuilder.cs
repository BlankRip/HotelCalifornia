using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] ScriptableLevelSeed seeder;
        [SerializeField] GameObject startRoomObj;
        [SerializeField] int iterations = 3;
        [SerializeField] int finalIteration = 2;
        [SerializeField] int maxRetries = 15;
        [SerializeField] int puzzlePairs = 2, singelPuzzles = 1;
        [SerializeField] int maxNumberOfSameRooms = 2;
        [SerializeField] LayerMask roomLayerMask;
        [SerializeField] List<GameObject> corridors;
        [SerializeField] List<GameObject> allRoomObjs;

        private BuildingData current;
        private BuildingData backup;
        private IRoom startRoom;

        private void Start() {
            current = new BuildingData();
            backup = new BuildingData();
            StartCoroutine(GenerateLevel());
        }

        private IEnumerator GenerateLevel() {
            WaitForSeconds longInterval = new WaitForSeconds(1);
            WaitForFixedUpdate interval = new WaitForFixedUpdate();

            yield return longInterval;
            SpawnStartRoom();
            yield return interval;
            IBuilder baseBuilder = new BaseLayoutBuilder(seeder, startRoom, iterations, finalIteration, ref current, ref backup);
            baseBuilder.StartBuilder();
            yield return interval;
            while(baseBuilder.GetBuilderStatus()) {
                if(baseBuilder.HasFailed())
                    Debug.Log("Failed");
                else
                    yield return interval;
            }

        }

        private void SpawnStartRoom() {
            GameObject sapwned = GameObject.Instantiate(startRoomObj.gameObject, transform.position, transform.rotation);
            sapwned.transform.parent = this.transform;
            startRoom = sapwned.GetComponent<IRoom>();
        }
    }
}