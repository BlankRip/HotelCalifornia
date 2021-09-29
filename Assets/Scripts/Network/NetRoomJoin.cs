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
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new JoinRoomData("joinRandom", DistributionOption.serveMe, "defaultRoom", 5, false)));
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
        public JoinRoomData(string eventName, string distributionOption, string roomName, int roomSize, bool isReserved)
        {
            this.eventName = eventName;
            this.distributionOption = distributionOption;
            this.roomName = roomName;
            this.roomSize = roomSize;
            this.isReserved = isReserved;
        }
    }
}