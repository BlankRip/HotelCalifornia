using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.Abilities;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.QuickDelivery {
    public class QuickDeliveryPuzzle : MonoBehaviour
    {
        [SerializeField] string myPossiblePosTag;
        [SerializeField] string objSpawnPosTag;
        [SerializeField] List<GameObject> spawnableItems;
        [SerializeField] string instructionTextPoolTag;
        [SerializeField] Transform instructionPos;
        [SerializeField] Transform timerPos;

        private List<GameObject> spawnedObjs;
        private List<GameObject> textObjs;
        private string delivaryItemName;
        private int amountToDeliver = 3;
        private int deliverd = 0;
        private float existanceTime = 60;
        private TextMeshProUGUI timerText;
        private float timerTextSize = 16;

        private void Start() {
            PlaceDeliveryObj();
            SpawnDeliveryItems();
            SetUpTexts();
        }

        private void PlaceDeliveryObj() {
            List<Transform> gos = GameObject.FindGameObjectWithTag(myPossiblePosTag).GetComponent<TransformListHolder>().GetList();
            Transform targetPos = gos[Random.Range(0, gos.Count)];
            transform.position = targetPos.position;
        }

        private void SpawnDeliveryItems() {
            List<Transform> spawnPoints = GameObject.FindGameObjectWithTag(objSpawnPosTag).GetComponent<TransformListHolder>().GetList();
            GameObject objToSpawn = spawnableItems[Random.Range(0, spawnableItems.Count)];
            delivaryItemName = objToSpawn.name;
            
            SpawnAtGivenPoints spawner = new SpawnAtGivenPoints(spawnPoints, objToSpawn, amountToDeliver, true);
            spawnedObjs = spawner.SpawnAndGetSpawnedObj();
        }

        private void SetUpTexts() {
            textObjs = new List<GameObject>();
            timerText = ObjectPool.instance.SpawnPoolObj(instructionTextPoolTag, instructionPos.position, instructionPos.rotation).GetComponent<TextMeshProUGUI>();
            timerText.text = $"Deliver 3 '{delivaryItemName}' for a reward";
            textObjs.Add(timerText.gameObject);

            timerText = ObjectPool.instance.SpawnPoolObj(instructionTextPoolTag, timerPos.position, timerPos.rotation).GetComponent<TextMeshProUGUI>();
            timerText.fontSize = timerTextSize;
            timerText.text = "60";
            textObjs.Add(timerText.gameObject);
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
            foreach(GameObject obj in textObjs)
                obj.SetActive(false);
            Destroy(this.gameObject, 0.1f);
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("DeliveryItem")) {
                deliverd++;
                spawnedObjs.Remove(other.gameObject);
                if(deliverd == amountToDeliver) {
                    Debug.Log("Solved");
                    if(other.GetComponent<DeliveryItem>().localHeld)
                        GetComponent<HumanAbilitySwaper>().Swap();
                    SelfDistruct();
                }
                Destroy(other.gameObject);
            }
        }
    }
}