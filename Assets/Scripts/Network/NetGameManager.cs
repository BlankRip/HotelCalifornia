using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Network
{
    public class NetGameManager : MonoBehaviour
    {
        public void Hear(string dataString)
        {
            string eventName = JsonUtility.FromJson<ReadyData>(dataString).eventName;
            switch (eventName)
            {
                case "readyRegistered":
                    Debug.Log("READY");
                    break;
                case "unReadyRegistered":
                    Debug.Log("UNREADY");
                    break;
            }
        }

        public void Ready()
        {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new ReadyData("iAmReady")));
        }

        public void Unready()
        {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new ReadyData("iAmNotReady")));
        }
    }

    [System.Serializable]
    public class ReadyData
    {
        public string eventName;
        public string distributionOption;
        public ReadyData(string name)
        {
            eventName = name;
            distributionOption = "serveMe";
        }
    }
}