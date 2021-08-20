using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen {
    public interface IRoomPlacer
    {
        bool PlaceRoom(List<GameObject> availableRooms);
        bool PlaceRoom(ref List<GameObject> availableRooms);
    }
}
