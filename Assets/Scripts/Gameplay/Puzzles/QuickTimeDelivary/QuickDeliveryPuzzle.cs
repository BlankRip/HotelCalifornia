using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.Abilities;
using TMPro;
using Knotgames.Audio;

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
            GameObject[] holders = GameObject.FindGameObjectsWithTag(myPossiblePosTag);
            List<Transform> gos = holders[KnotRandom.theRand.Next(0, holders.Length)].GetComponent<TransformListHolder>().GetList();
            Transform targetPos = gos[KnotRandom.theRand.Next(0, gos.Count)];
            transform.position = targetPos.position;
        }

        private void SpawnDeliveryItems() {
            GameObject[] holders = GameObject.FindGameObjectsWithTag(objSpawnPosTag);
            List<Transform> spawnPoints = new List<Transform>();
            foreach(GameObject go in holders) {
                if(spawnPoints.Count < 13) {
                    List<Transform> thisHolderData = go.GetComponent<TransformListHolder>().GetList();
                    foreach (Transform item in thisHolderData)
                        spawnPoints.Add(item);
                }
            }
            GameObject objToSpawn = spawnableItems[KnotRandom.theRand.Next(0, spawnableItems.Count)];
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
                AudioPlayer.instance.PlayAudio2DOneShot(ClipName.DeliveryDone);
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