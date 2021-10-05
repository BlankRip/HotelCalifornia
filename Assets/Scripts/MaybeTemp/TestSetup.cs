using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;
using Knotgames.LevelGen;

namespace Knotgames.Gameplay {
    public class TestSetup : MonoBehaviour
    {
        [SerializeField] GameObject loadingScreen;
        [SerializeField] GameObject spawnerObj;
        [SerializeField] ScriptableLevelSeed gameSeed;
        [SerializeField] GameObject objToTurnOn;
        private bool host = false;

        private void Start() {
            SendAtLoadingStatus();
            NetConnector.instance.OnMsgRecieve.AddListener(Hear);
        }

        private void OnDestroy() {
            NetConnector.instance.OnMsgRecieve.RemoveListener(Hear);
        }

        private void SendAtLoadingStatus() {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new ReadyData("readyToGenerate", DistributionOption.serveMe)));
        }

        public void Hear(string dataString)
        {
            string eventName = JsonUtility.FromJson<EventNetData>(dataString).eventName;
            switch (eventName)
            {
                case "generateSeed": {
                    host = true;
                    gameSeed.levelSeed.GenerateSeed();
                    gameSeed.levelSeed.SeedSuccesful();
                    break;
                }
                case "replicateLevel": {
                    if(!host) {
                        int seed = JsonUtility.FromJson<RecievedSeed>(dataString).seed;
                        gameSeed.levelSeed.SetSeed(seed);
                        gameSeed.levelSeed.SeedSuccesful();
                    }
                    break;
                }
                case "playGame": {
                    Debug.LogError("START NOW");
                    gameSeed.levelSeed.Initilize();
                    objToTurnOn.SetActive(true);
                    spawnerObj.SetActive(true);
                    loadingScreen.SetActive(false);
                    break;
                }
            }
        }

        private class RecievedSeed {
            public int seed;
        }
    
    }
}