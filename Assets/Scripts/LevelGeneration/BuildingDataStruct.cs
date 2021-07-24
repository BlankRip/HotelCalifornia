using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public struct BuildingData 
    {
        public List<RoomType> allRoomTypes;
        public List<int> eachTypeSpawned;
        public List<IRoom> currentRoutRooms;
        public List<Transform> availableDoorways;
        public int currentIterations;
        public int retries;

        public List<IRoom> availableSide1Rooms;
        public List<IRoom> availableSide2Rooms;
        public List<IRoom> availableSide3Rooms;

        public BuildingData(bool needed = true) {
            allRoomTypes = new List<RoomType>();
            eachTypeSpawned = new List<int>();
            currentRoutRooms = new List<IRoom>();
            availableDoorways = new List<Transform>();
            currentIterations = 0;
            retries = 0;

            availableSide1Rooms = new List<IRoom>();
            availableSide2Rooms = new List<IRoom>();
            availableSide3Rooms = new List<IRoom>();
        }
    }
}