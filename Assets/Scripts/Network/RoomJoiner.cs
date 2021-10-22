using System.Collections;
using System.Collections.Generic;
using Knotgames.Network;
using UnityEngine;
using Knotgames.Extensions;

namespace Knotgames.Network
{
    public class RoomJoiner : MonoBehaviour
    {
        public void JoinRoom()
        {
            NetRoomJoin.instance.JoinRandomRoom();
        }

        public void Ready()
        {
            NetGameManager.instance.Ready();
        }

        public void LeaveRoom()
        {
            NetGameManager.instance.LeaveRoom();
        }

        public void CreateRoom()
        {
            NetGameManager.instance.CreateRoom();
        }

        public void Unready()
        {
            NetGameManager.instance.Unready();
        }

        public void JoinWithID()
        {
            NetGameManager.instance.JoinWithID();
        }

        public void CopyRoomID()
        {
            ClipboardExtensions.CopyToClipboard(NetRoomJoin.instance.roomID.value);
        }
    }
}