using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Network
{
    public class NetRoomPlayersRecord : MonoBehaviour, INetRoomPlayersRecord
    {
        Dictionary<string, INetObject> roomPlayers = new Dictionary<string, INetObject>();

        public void RegisterPlayer(string playerID, INetObject playerObject)
        {
            roomPlayers[playerID] = playerObject;
        }

        public void RemovePlayer(string playerID)
        {
         ///   roomPlayers[playerID].is;
        }
    }
}
