using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Knotgames.Extensions;
using Knotgames.CharacterData;

namespace Knotgames.Network
{
    public class NetGameManager : MonoBehaviour
    {
        [HideInInspector] public bool inGame = false;
        public ScriptableSpawnDataCollection allSpawnData;
        public ScriptableCharacterSelect characterData;
        public static NetGameManager instance;
        bool winDone = false;
        public bool humanWin;
        NetObject ghost;
        List<NetObject> humans = new List<NetObject>();
        [HideInInspector] public List<SpawnData> ghostModels = new List<SpawnData>();
        [HideInInspector] public List<SpawnData> humanModels = new List<SpawnData>();

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
                    inGame = true;
                    SceneManager.LoadScene(1);
                    break;
                case "playerLeftRoom":
                    if (inGame && !winDone)
                    {
                        UnityEngine.Debug.Log("<color=red>A SINFUL BEING HAS BEEN PURGED FROM THE LOBBY, WHAT A DICK</color>");
                        inGame = false;
                        string leftID = JsonUtility.FromJson<PlayerIDExtractor>(dataString).playerID;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        NetGameManager.instance.LeaveRoom();
                        SceneManager.LoadScene(2);
                    }
                    else if (inGame && winDone)
                    {
                        UnityEngine.Debug.LogError("RESETTING WIN");
                        inGame = false;
                        winDone = false;
                        ghost = null;
                        humans.Clear();
                        ghostModels.Clear();
                        humanModels.Clear();
                        string leftID = JsonUtility.FromJson<PlayerIDExtractor>(dataString).playerID;
                    }
                    else
                    {
                        string leftID = JsonUtility.FromJson<PlayerIDExtractor>(dataString).playerID;
                        UnityEngine.Debug.Log("<color=yellow>A SINFUL BEING HAS BEEN PURGED FROM THE LOBBY, ATLEAST HE LEFT EARLY</color>");
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        NetGameManager.instance.LeaveRoom();
                        SceneManager.LoadScene(2);
                    }
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
                    break;
                case "newPlayerJoinedRoom":
                    UnityEngine.Debug.LogError("<color=white>PLAYER JOINED ROOM</color>");
                    string joinedID = JsonUtility.FromJson<PlayerIDExtractor>(dataString).playerID;
                    break;
                case "toggledWin":
                    UnityEngine.Debug.LogError("<color=white>WIN TRIGGERED</color>");
                    winDone = true;
                    humanWin = JsonUtility.FromJson<WinData>(dataString).humanWin;
                    UnityEngine.Debug.LogError($"<color=white>HUMANS WON: {humanWin}</color>");
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Scener.LoadScene(3);
                    break;
                case "syncObjectData":
                    if (JsonUtility.FromJson<ObjectNetData>(dataString).componentType == "ModelSpawnNetData")
                    {
                        ModelSpawnNetData temp = JsonUtility.FromJson<ModelSpawnNetData>(dataString);
                        switch (CustomExtensions.GetModelType(temp.modelType))
                        {
                            case "ghost":
                                ghostModels.Add(CustomExtensions.ReturnModelObject(temp.modelType));
                                UnityEngine.Debug.LogError("GHOST MODEL ADDED");
                                break;
                            case "human":
                                humanModels.Add(CustomExtensions.ReturnModelObject(temp.modelType));
                                UnityEngine.Debug.LogError("HUMAN MODEL ADDED");
                                break;
                        }
                    }
                    break;
                case "roomFull":
                    UnityEngine.Debug.LogError("FULL ROOM");
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
            UnityEngine.Debug.LogError($"CALLING LEAVE ROOM on '{NetRoomJoin.instance.roomID.value}'");
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
            NetRoomJoin.instance.roomID.value = ClipboardExtensions.GetClipboard();
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
            this.humanWin = humanWin;
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
    [System.Serializable]
    public class PlayerTypeExtractor
    {
        public string playerType;
    }
}