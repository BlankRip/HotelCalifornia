using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Knotgames.Network
{
    public class NetGameManager : MonoBehaviour
    {
        public void Hear(string dataString)
        {
            string eventName = JsonUtility.FromJson<ReadyData>(dataString).eventName;
            switch (eventName)
            {
                case "preparePlayers":
                    Debug.LogError("PREPARING");
                    break;
                case "startGame":
                    Debug.LogError("STARTING");
                    SceneManager.LoadScene(1);
                    break;
            }
        }

        public void Ready()
        {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new ReadyData("iAmReady", DistributionOption.serveMe)));
        }

        public void Unready()
        {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new ReadyData("iAmNotReady", DistributionOption.serveMe)));
        }
    }

    [System.Serializable]
    public class ReadyData
    {
        public string eventName;
        public string distributionOption;
        public ReadyData(string name, string distributionOption)
        {
            eventName = name;
            this.distributionOption = distributionOption;
        }
    }
}