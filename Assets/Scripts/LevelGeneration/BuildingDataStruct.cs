using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    [System.Serializable]
    public class BuilderData {
        public Transform parent;
        public GameObject startRoomObj;
        
        public LayerMask roomLayerMask;
        public List<GameObject> corridors;
        public List<GameObject> allRoomObjs;

        public int iterations = 3;
        public int finalIteration = 2;
        public int maxRetries = 15;
        public int puzzlePairs = 2, singelPuzzles = 1;
        public int maxNumberOfSameRooms = 2;

        public List<IRoom> availableSide1Rooms;
        public List<IRoom> availableSide2Rooms;
        public List<IRoom> availableSide3Rooms;

        public void GetAllUsableRooms(BuildingStatus buildingStatus, ref List<GameObject> rooms) {
            for (int i = 0; i < allRoomObjs.Count; i++) {
                IRoom current = allRoomObjs[i].GetComponent<IRoom>();
                if(buildingStatus.eachTypeSpawned[buildingStatus.allRoomTypes.IndexOf(current.GetRoomType())] < maxNumberOfSameRooms)
                    rooms.Add(allRoomObjs[i]);
            }
        }
    }

    [System.Serializable]
    public class BuildingStatus
    {
        public List<IRoom> allRooms;
        public List<RoomType> allRoomTypes;
        public List<int> eachTypeSpawned;
        public List<IRoom> currentRoutRooms;
        public List<Transform> availableDoorways;
        public int currentIterations;
        public int retries;

        public BuildingStatus() {
            allRoomTypes = new List<RoomType>();
            eachTypeSpawned = new List<int>();
            currentRoutRooms = new List<IRoom>();
            availableDoorways = new List<Transform>();
            currentIterations = 0;
            retries = 0;
        }

        public BuildingStatus(BuildingStatus copy) {
            allRoomTypes = new List<RoomType>(copy.allRoomTypes);
            eachTypeSpawned = new List<int>(copy.eachTypeSpawned);
            currentRoutRooms = new List<IRoom>(copy.allRooms);
            availableDoorways = new List<Transform>(copy.availableDoorways);
            currentIterations = copy.currentIterations;
            retries = copy.currentIterations;
        }
    }
}