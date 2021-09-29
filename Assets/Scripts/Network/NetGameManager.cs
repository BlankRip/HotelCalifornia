using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Knotgames.Extensions;

namespace Knotgames.Network
{
    public class NetGameManager : MonoBehaviour
    {
        [HideInInspector] public bool inGame = false;
        public static NetGameManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        public void Hear(string dataString)
        {
            string eventName = JsonUtility.FromJson<ReadyData>(dataString).eventName;
            switch (eventName)
            {
                case "preparePlayers":
                    UnityEngine.Debug.LogError("PREPARING");
                    break;
                case "startGame":
                    UnityEngine.Debug.LogError("STARTING");
                    inGame = true;
                    SceneManager.LoadScene(1);
                    break;
                case "playerLeftRoom":
                    if (inGame)
                    {
                        UnityEngine.Debug.Log("<color=red>A SINFUL BEING HAS BEEN PURGED FROM THE LOBBY, WHAT A DICK</color>");
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        SceneManager.LoadScene(2);
                    }
                    else
                        UnityEngine.Debug.Log("<color=yellow>A SINFUL BEING HAS BEEN PURGED FROM THE LOBBY, ATLEAST HE LEFT EARLY</color>");
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
            NetRoomJoin.instance.roomID.value = "";
            NetRoomJoin.instance.roomID.value.CopyToClipboard();
        }

        public void CopyRoomID()
        {
            NetRoomJoin.instance.roomID.value.CopyToClipboard();
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