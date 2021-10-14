using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Network
{
    public class NetEventHub : INetEventHub
    {

        SOBool isConnected;
        SOString playerID;


        public NetEventHub(SOBool isConnected, SOString playerID)
        {
            this.isConnected = isConnected;
            this.playerID = playerID;
        }

        public void Listen(string dataString)
        {
            string eventName = JsonUtility.FromJson<BaseDataExtractor>(dataString).eventName;
            NetDebug.instance.AddDebug(dataString);

            switch (eventName)
            {
                case "connected":
                    isConnected.value = true;
                    playerID.value = JsonUtility.FromJson<PlayerJoin>(dataString).playerID;
                    break;

                case "registered":

                    break;

                case "joinedRandomRoom":
                    NetRoomJoin.instance.roomID.value = JsonUtility.FromJson<RoomID>(dataString).roomID;
                    break;

                case "playerLeftRoom":
                    UnityEngine.Debug.LogError("Player Left Room : " + dataString);
                    break;

                case "exitRoom":

                    break;

                case "syncObjectData":
                    string objectID = JsonUtility.FromJson<ObjectID>(dataString).objectID;
                    NetObjManager.instance.PassDataToObject(objectID, dataString);
                    break;

                case "roomStateSync":
                    NetUnityEvents.instance.roomTiggerOnMsgRecieve.Invoke(dataString);
                    break;
                case "potionTransform":
                    NetUnityEvents.instance.portionTransform.Invoke(dataString);
                    break;
                case "potionUseStatus":
                    NetUnityEvents.instance.portionUseStatus.Invoke(dataString);
                    break;
                case "startMixer":
                    NetUnityEvents.instance.mixerEvents.Invoke(dataString);
                    break;
                case "interfearMixer":
                    NetUnityEvents.instance.mixerEvents.Invoke(dataString);
                    break;
                case "puzzleSolveButton":
                    NetUnityEvents.instance.puzzleSolvedEvent.Invoke(dataString); 
                    break;

                case "XOBoardPiece":
                    NetUnityEvents.instance.xoPieceEvent.Invoke(dataString);
                    break;

                case "MorseButton":
                    NetUnityEvents.instance.morseButtonEvent.Invoke(dataString);
                    break;
                case "InterferePlayer":
                    NetUnityEvents.instance.morsePlayerEvent.Invoke(dataString);
                    break;

                case "spawnObject":
                    NetObjManager.instance.AddSpawnObject(dataString);
                    break;

                case "destroyObject":
                    NetObjManager.instance.AddDespawnObject(
                        JsonUtility.FromJson<NetDataBase>(dataString)
                    );
                    break;

            }

        }
    }

    public class BaseDataExtractor
    {
        public string eventName;
    }

    public class PlayerJoin
    {
        public string playerID;
    }

    public class ObjectID
    {
        public string objectID;
    }

    public class RoomID
    {
        public string roomID;
    }
}

