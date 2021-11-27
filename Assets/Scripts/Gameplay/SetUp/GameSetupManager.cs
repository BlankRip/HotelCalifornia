using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.LevelGen;
using Knotgames.Network;

namespace Knotgames.Gameplay {
    public class GameSetupManager : MonoBehaviour
    {
        [SerializeField] GameObject loadingScreen;
        [SerializeField] GameObject spawnerObj;
        [SerializeField] ScriptableLevelSeed gameSeed;
        [SerializeField] ScriptableLevelBuilder builder;
        [SerializeField] ScriptableHostStatus hostStatus;
        [SerializeField] GameplayEventCollection eventCollection;

        private void Start() {
            hostStatus.isHost = false;
            IdsResetter.ResetIDs();
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
                    hostStatus.isHost = true;
                    builder.levelBuilder.StartLevelGen(true);
                    break;
                }
                case "replicateLevel": {
                    if(!hostStatus.isHost) {
                        Debug.LogError("Replicating");
                        int seed = JsonUtility.FromJson<RecievedSeed>(dataString).seed;
                        gameSeed.levelSeed.SetSeed(seed);
                        builder.levelBuilder.StartLevelGen(false);
                    }
                    break;
                }
                case "playGame": {
                    Debug.LogError("START NOW");
                    spawnerObj.SetActive(true);
                    loadingScreen.SetActive(false);
                    eventCollection.gameStart.Invoke();
                    Destroy(this, .4f);
                    break;
                }
            }
        }

        private class RecievedSeed {
            public int seed;
        }
    }

}