using System.Collections;
using System.Collections.Generic;
using Knotgames.Network;
using UnityEngine;

namespace Knotgames.Network
{
    public class RoomJoiner : MonoBehaviour
    {
        public void JoinRoom()
        {
            NetRoomJoin.instance.JoinRandomRoom();
        }
    }
}