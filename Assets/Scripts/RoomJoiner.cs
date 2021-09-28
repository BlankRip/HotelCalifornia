using System.Collections;
using System.Collections.Generic;
using Knotgames.Network;
using UnityEngine;

public class RoomJoiner : MonoBehaviour
{
    public void JoinRoom()
    {
        NetRoomJoin.instance.JoinRandomRoom();
    }
}