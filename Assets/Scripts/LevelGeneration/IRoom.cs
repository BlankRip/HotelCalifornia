using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public enum RoomType { Nada, A, B, C, D, E, F, G, H }
    public interface IRoom {
        void SelfKill();
        RoomType GetRoomType();
        Transform GetTransform();
        List<Bounds> GetRoomBounds();
        List<Collider> GetColliders();
        List<Transform> GetDoorways();
        GameObject GetPuzzleVarient();
        GameObject GetSingleVarient();
    }
}