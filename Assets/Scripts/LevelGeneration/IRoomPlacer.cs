using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public interface IRoomPlacer
    {
        bool PlaceRoom(List<GameObject> availableRooms);
        bool PlaceRoom(ref List<GameObject> availableRooms);
    }
}
