using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Knotgames.Extensions;
using Knotgames.CharacterData;
using System;

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
        [HideInInspector] public List<SpawnData> ghostModels = new List<SpawnData>();
        [HideInInspector] public List<SpawnData> humanModels = new List<SpawnData>();
        private bool createdRoom = false;

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
                    ghostModels.Clear();
                    humanModels.Clear();
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
                        if (SceneManager.GetActiveScene().name == "EndScene")
                        {
                            ghostModels.Clear();
                            humanModels.Clear();
                        }
                    }
                    else
                    {
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
                    LeaveRoom();
                    StartCoroutine(DelayedWinStuff(dataString));
                    break;
                case "syncObjectData":
                    if (JsonUtility.FromJson<ObjectNetData>(dataString).componentType == "ModelSpawnNetData")
                    {
                        ModelSpawnNetData temp = JsonUtility.FromJson<ModelSpawnNetData>(dataString);
                        switch (CustomExtensions.GetModelType(temp.modelType))
                        {
                            case "ghost":
                                ghostModels.Add(CustomExtensions.ReturnModelObject(temp.modelType));
                                UnityEngine.Debug.LogError($"ADDED {CustomExtensions.ReturnModelObject(temp.modelType).model.name}");
                                break;
                            case "human":
                                humanModels.Add(CustomExtensions.ReturnModelObject(temp.modelType));
                                UnityEngine.Debug.LogError($"ADDED {CustomExtensions.ReturnModelObject(temp.modelType).model.name}");
                                break;
                        }
                    }
                    break;
                case "roomFull":
                    UnityEngine.Debug.LogError("FULL ROOM");
                    break;
                case "timerOff":
                    UnityEngine.Debug.LogError("<color=purple>GAME TIMER TURNED OFF</color>");
                    break;
            }
        }

        private IEnumerator DelayedWinStuff(string dataString)
        {
            yield return new WaitForSeconds(0.3f);
            humanWin = JsonUtility.FromJson<WinData>(dataString).humanWin;
            UnityEngine.Debug.LogError($"<color=white>HUMANS WON: {humanWin}</color>");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Scener.LoadScene(3);
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
            if (NetRoomJoin.instance.roomID.value != null)
            {
                createdRoom = false;
                NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new ReadyData("leaveRoom", DistributionOption.serveMe)));
                NetRoomJoin.instance.roomID.value = "";
                NetRoomJoin.instance.roomID.value.CopyToClipboard();
            }
        }

        public void CreateRoom()
        {
            if (!createdRoom)
            {
                NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new JoinRoomData("createCustomRoom", DistributionOption.serveMe, "customRoom", 3, true)));
                createdRoom = true;
                CopyRoomID();
            }
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
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new GameOver()));
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
    public class GameOver
    {
        public string eventName;
        public string distributionOption;
        public string roomID;
        public GameOver()
        {
            eventName = "gameOver";
            distributionOption = DistributionOption.serveMe;
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
    [System.Serializable]
    public class PlayerTypeExtractor
    {
        public string playerType;
    }
}