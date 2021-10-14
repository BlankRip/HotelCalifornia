using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.QuickDelivery {
    public class QuickDeliveryPuzzle : MonoBehaviour
    {
        [SerializeField] string myPossiblePosTag;
        [SerializeField] string objSpawnPosTag;
        [SerializeField] List<GameObject> spawnableItems;
        [SerializeField] string instructionTextPoolTag;

        private List<GameObject> spawnedObjs;
        private string delivaryItemName;
        private int amountToDeliver = 3;
        private int deliverd = 0;
        private float existanceTime = 60;
        private TextMeshProUGUI timerText;

        private void Start() {
            PlaceDeliveryObj();
            SpawnDeliveryItems();
        }

        private void PlaceDeliveryObj() {
            GameObject[] gos = GameObject.FindGameObjectsWithTag(myPossiblePosTag);
            Transform targetPos = gos[Random.Range(0, gos.Length)].transform;
            transform.position = targetPos.position;
        }

        private void SpawnDeliveryItems() {
            GameObject[] gos = GameObject.FindGameObjectsWithTag(objSpawnPosTag);
            List<Transform> spawnPoints = new List<Transform>();
            foreach(GameObject go in gos)
                spawnPoints.Add(go.transform);
            GameObject objToSpawn = spawnableItems[Random.Range(0, spawnableItems.Count)];
            delivaryItemName = objToSpawn.name;
            
            SpawnAtGivenPoints spawner = new SpawnAtGivenPoints(spawnPoints, objToSpawn, amountToDeliver, true);
            spawnedObjs = spawner.SpawnAndGetSpawnedObj();
        }

        private void Update() {
            existanceTime -= Time.deltaTime;
            if(existanceTime <= 0)
                SelfDistruct();
            else
                timerText.text = ((int)existanceTime).ToString();
        }

        private void SelfDistruct() {
            foreach(GameObject obj in spawnedObjs)
                Destroy(obj);
            Destroy(this.gameObject);
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("DeliveryItem")) {
                deliverd++;
                spawnedObjs.Remove(other.gameObject);
                Destroy(other.gameObject);
                if(deliverd == amountToDeliver) {
                    
                }
            }
        }
    }
}