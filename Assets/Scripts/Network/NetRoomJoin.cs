using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Network
{
    public class NetRoomJoin : MonoBehaviour
    {
        public static NetRoomJoin instance;
        public SOString roomID;

        void Awake()
        {
            if (instance == null) instance = this;
        }

        void Start()
        {
            roomID.value = null;
        }

        public void JoinRandomRoom()
        {
            NetConnector.instance.SendDataToServer(
                JsonUtility.ToJson(
                    new JoinRoomData()
                    {
                        eventName = "joinRandom",
                        distributionOption = DistributionOption.serveMe,
                        roomName = "defaultRoom",
                        roomSize = 5,
                        isReserved = false
                    }
                )
            );
        }
    }


    [System.Serializable]
    public class JoinRoomData
    {
        public string eventName;
        public string distributionOption;
        public string roomName;
        public int roomSize;
        public bool isReserved;
    }

}