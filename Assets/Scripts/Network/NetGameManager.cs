using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Knotgames.Network
{
    public class NetGameManager : MonoBehaviour
    {
        [HideInInspector] public bool inGame = false;
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
                    inGame = true;
                    SceneManager.LoadScene(1);
                    break;
                case "playerLeftRoom":
                    if (inGame)
                    {
                        Debug.Log("<color=red>A SINFUL BEING HAS BEEN PURGED FROM THE LOBBY, WHAT A DICK</color>");
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        SceneManager.LoadScene(2);
                    }
                    else
                        Debug.Log("<color=yellow>A SINFUL BEING HAS BEEN PURGED FROM THE LOBBY, ATLEAST HE LEFT EARLY</color>");
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

        public void LeaveRoom()
        {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new ReadyData("leaveRoom", DistributionOption.serveMe)));
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