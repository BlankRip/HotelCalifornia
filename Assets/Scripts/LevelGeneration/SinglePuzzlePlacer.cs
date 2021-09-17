using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen {
    public class SinglePuzzlePlacer: IPuzzlePlacer
    {
        private BuilderData builderData;
        private List<PuzzleType> spawnedSingleTypes;
        private int setInUse;

        public SinglePuzzlePlacer(ref BuilderData builderData) {
            this.builderData = builderData;

            spawnedSingleTypes = new List<PuzzleType>();
        }

        public bool Place() {
            bool puzzlePlaced = false;
            while(!puzzlePlaced) {
                List<IRoom> currentSet = GetRandomSet();
                int rand = Random.Range(0, currentSet.Count);
                ISingelPuzzleRoom spawned = GameObject.Instantiate(currentSet[rand].GetSingleVarient(), 
                    currentSet[rand].GetTransform().position, currentSet[rand].GetTransform().rotation,
                    currentSet[rand].GetTransform().parent).GetComponent<ISingelPuzzleRoom>();

                PuzzleType validPuzzle = spawned.GetAndActivePuzzle(ref spawnedSingleTypes);
                if(validPuzzle == PuzzleType.Nada) {
                    spawned.SelfKill();
                } else {
                    CleanUpReplacedRoom(currentSet[rand], setInUse);
                    puzzlePlaced = true;
                }
            }
            return true;
        }

        private List<IRoom> GetRandomSet() {
            //setInUse = Random.Range(0, 3);
            setInUse = int.MinValue;
            CheckForFreeRooms(builderData.availableSide1Rooms, 0, ref setInUse);
            CheckForFreeRooms(builderData.availableSide2Rooms, 1, ref setInUse);
            CheckForFreeRooms(builderData.availableSide3Rooms, 2, ref setInUse);

            switch(setInUse) {
                case 0:
                    return builderData.availableSide1Rooms;
                case 1:
                    return builderData.availableSide2Rooms;
                case 2:
                    return builderData.availableSide3Rooms;
                default:
                    return null;
            }
        }

        private void CheckForFreeRooms(List<IRoom> rooms, int id, ref int freeRoomsCount) {
            int temp = rooms.Count;
            if(temp > freeRoomsCount)
                freeRoomsCount = id;
        }

        private void RemoveFormSet(IRoom room, int setId) {
            switch(setId) {
                case 0:
                    builderData.availableSide1Rooms.Remove(room);
                    break;
                case 1:
                    builderData.availableSide2Rooms.Remove(room);
                    break;
                case 2:
                    builderData.availableSide3Rooms.Remove(room);
                    break;
            }
        }

        private void CleanUpReplacedRoom(IRoom room, int setId) {
            RemoveFormSet(room, setId);
            room.SelfKill();
        }

        private struct ReplacedRoomData {
            int inSet;
            IRoom room;

            public ReplacedRoomData(IRoom room, int inSet) {
                this.room = room;
                this.inSet = inSet;
            }
        }
    }
}
