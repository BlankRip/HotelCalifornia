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
        public List<string> connectedPlayers;
        bool winDone = false;
        public bool humanWin;
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
                    if (inGame && !winDone)
                    {
                        UnityEngine.Debug.Log("<color=red>A SINFUL BEING HAS BEEN PURGED FROM THE LOBBY, WHAT A DICK</color>");
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        SceneManager.LoadScene(2);
                    }
                    else if (inGame && winDone)
                    {
                        UnityEngine.Debug.LogError("RESETTING WIN");
                        winDone = false;
                        inGame = false;
                    }
                    else
                        UnityEngine.Debug.Log("<color=yellow>A SINFUL BEING HAS BEEN PURGED FROM THE LOBBY, ATLEAST HE LEFT EARLY</color>");
                    break;
                case "youLeftRoom":
                    UnityEngine.Debug.LogError("<color=yellow>YOU LEFT ROOM</color>");
                    break;
                case "customRoomCreated":
                    UnityEngine.Debug.LogError("<color=green>CUSTOM ROOM CREATED</color>");
                    string roomID = JsonUtility.FromJson<RoomExtractor>(dataString).roomID;
                    NetRoomJoin.instance.roomID.value = roomID;
                    break;
                case "roomDoesNotExist":
                    UnityEngine.Debug.LogError("<color=red>ROOM DOES NOT EXIST</color>");
                    break;
                case "joinedRoomOfID":
                    UnityEngine.Debug.LogError("<color=green>CUSTOM ROOM JOINED</color>");
                    break;
                case "joinedRandomRoom":
                    UnityEngine.Debug.LogError("<color=white>JOINED RANDOM ROOM</color>");
                    connectedPlayers.Add(NetConnector.instance.playerID.value);
                    break;
                case "roomFull":
                    UnityEngine.Debug.LogError("<color=blue>ROOM FULL</color>");
                    break;
                case "playerJoinedRoom":
                    UnityEngine.Debug.LogError("<color=white>PLAYER JOINED ROOM</color>");
                    string joinedID = JsonUtility.FromJson<PlayerIDExtractor>(dataString).playerID;
                    connectedPlayers.Add(joinedID);
                    break;
                case "toggledWin":
                    UnityEngine.Debug.LogError("<color=white>WIN TRIGGERED</color>");
                    winDone = true;
                    humanWin = JsonUtility.FromJson<WinData>(dataString).humanWin;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Scener.LoadScene(3);
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

        public void CreateRoom()
        {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new JoinRoomData("createCustomRoom", DistributionOption.serveMe, "customRoom", 2, true)));
        }

        public void JoinWithID()
        {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new RoomData("joinRoomID", DistributionOption.serveMe, ClipboardExtensions.GetClipboard())));
        }

        public void CopyRoomID()
        {
            ClipboardExtensions.CopyToClipboard(NetRoomJoin.instance.roomID.value);
        }

        public void ToggleWinScreen(bool humanWin)
        {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new WinData(humanWin)));
        }

        public NetObject ReturnRandomPlayer()
        {
            string randomPlayer = connectedPlayers[Random.Range(0, connectedPlayers.Count)];
            foreach (NetObject obj in NetObjManager.instance.allNetObject)
            {
                if (obj.ownerID == randomPlayer)
                    return obj;
            }
            return null;
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

    [System.Serializable]
    public class RoomData
    {
        public string eventName;
        public string distributionOption;
        public string roomID;
        public RoomData(string name, string distributionOption, string ID)
        {
            eventName = name;
            this.distributionOption = distributionOption;
            roomID = ID;
        }
    }

    [System.Serializable]
    public class WinData
    {
        public string eventName;
        public string distributionOption;
        public string roomID;
        public bool humanWin;
        public WinData(bool humanWin)
        {
            eventName = "toggledWin";
            distributionOption = DistributionOption.serveAll;
            roomID = NetRoomJoin.instance.roomID.value;
        }
    }

    [System.Serializable]
    public class RoomExtractor
    {
        public string roomID;
    }

    [System.Serializable]
    public class PlayerIDExtractor
    {
        public string playerID;
    }
}