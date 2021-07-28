using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    [System.Serializable]
    public class BuilderData {
        public Transform parent;
        public GameObject levelGen;
        public GameObject startRoomObj;
        
        public LayerMask roomLayerMask;
        public List<GameObject> corridors;
        public List<GameObject> allRoomObjs;

        public int iterations = 3;
        public int finalIteration = 2;
        public int maxRetries = 15;
        public int puzzlePairs = 2, singelPuzzles = 1;
        public int maxNumberOfSameRooms = 2;

        public event System.Action onFail;
        public void OnFaile() {
            onFail();
        }

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
        public List<RoomType> allRoomTypes;
        public List<int> eachTypeSpawned;
        public List<IRoom> currentRoutRooms;
        [HideInInspector] public List<Transform> availableDoorways;
        [HideInInspector] public int retries;

        public List<IRoom> availableSide1Rooms;
        public List<IRoom> availableSide2Rooms;
        public List<IRoom> availableSide3Rooms;

        public BuildingStatus() {
            allRoomTypes = new List<RoomType>();
            eachTypeSpawned = new List<int>();
            currentRoutRooms = new List<IRoom>();
            availableDoorways = new List<Transform>();
            retries = 0;

            availableSide1Rooms = new List<IRoom>();
            availableSide2Rooms = new List<IRoom>();
            availableSide3Rooms = new List<IRoom>();
        }

        public BuildingStatus(BuildingStatus copy) {
            allRoomTypes = new List<RoomType>(copy.allRoomTypes);
            eachTypeSpawned = new List<int>(copy.eachTypeSpawned);
            currentRoutRooms = new List<IRoom>(copy.currentRoutRooms);
            availableDoorways = new List<Transform>(copy.availableDoorways);
            retries = copy.retries;
        }
    }
}