using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public class PairPuzzlePlacer: IPuzzlePlacer
    {
        private ScriptableLevelSeed seeder;
        private BuildingStatus currentBuildStatus;
        private List<PuzzleType> spawnedPairTypes;
        int setInUse;

        public PairPuzzlePlacer(ScriptableLevelSeed levelSeed, ref BuildingStatus currentBuildStatus) {
            seeder = levelSeed;
            this.currentBuildStatus = currentBuildStatus;

            spawnedPairTypes = new List<PuzzleType>();
        }

        public bool Place() {
            List<IRoom> currentSet = GetRandomSet();
            int rand = seeder.levelSeed.GetRandomBetween(0, currentSet.Count);
            
            IPairPuzzleRoom spawned = GameObject.Instantiate(currentSet[rand].GetPuzzleVarient(), 
                currentSet[rand].GetTransform().position, currentSet[rand].GetTransform().rotation, 
                currentSet[rand].GetTransform().parent).GetComponent<IPairPuzzleRoom>();
            
            CleanUpReplacedRoom(currentSet[rand], setInUse);
            
            List<int> available = new List<int>() {0, 1, 2};
            available.Remove(setInUse);
            bool placed = LoopThrewAndPlacePuzzle(available, spawned);

            if(placed) {
                seeder.levelSeed.UpdateSeed();
                return true;
            } else
                return false;
        }

        private List<IRoom> GetRandomSet() {
            setInUse = seeder.levelSeed.GetRandomBetween(0, 3);

            switch(setInUse) {
                case 0:
                    return currentBuildStatus.availableSide1Rooms;
                case 1:
                    return currentBuildStatus.availableSide2Rooms;
                case 2:
                    return currentBuildStatus.availableSide3Rooms;
                default:
                    return null;
            }
        }

        private void RemoveFormSet(IRoom room, int setId) {
            switch(setId) {
                case 0:
                    currentBuildStatus.availableSide1Rooms.Remove(room);
                    break;
                case 1:
                    currentBuildStatus.availableSide2Rooms.Remove(room);
                    break;
                case 2:
                    currentBuildStatus.availableSide3Rooms.Remove(room);
                    break;
            }
        }
        
        private void CleanUpReplacedRoom(IRoom room, int setId) {
            RemoveFormSet(room, setId);
            room.SelfKill();
        }

        private bool LoopThrewAndPlacePuzzle(List<int> listOfWays, IPairPuzzleRoom roomInEye) {
            for (int i = 0; i < listOfWays.Count; i++) {
                List<IRoom> roomList = GetRoomSet(listOfWays[i]);
                for (int j = 0; j < roomList.Count; j++) {
                    if(roomInEye.GetConnectableRoomTypes().Contains(roomList[j].GetRoomType())) {
                        IPairPuzzleRoom spawnedSoulmate = GameObject.Instantiate(roomList[j].GetPuzzleVarient(), 
                            roomList[j].GetTransform().position, roomList[j].GetTransform().rotation, 
                            roomList[j].GetTransform().parent).GetComponent<IPairPuzzleRoom>();

                        PuzzleType finalType = spawnedSoulmate.GetAndActivePuzzle(roomInEye.GetPuzzleTypes());

                        if(!spawnedPairTypes.Contains(finalType)) {
                            roomInEye.ActivatePuzzleOfType(finalType);
                            spawnedPairTypes.Add(finalType);
                        } else {
                            return false;
                        }
                        CleanUpReplacedRoom(roomList[j], listOfWays[i]);
                        return true;
                    }
                }
            }

            return false;
        }

        private List<IRoom> GetRoomSet(int id) {
            switch(id) {
                case 0:
                    return currentBuildStatus.availableSide1Rooms;
                case 1:
                    return currentBuildStatus.availableSide2Rooms;
                case 2:
                    return currentBuildStatus.availableSide3Rooms;
                default:
                    return null;
            }
        }
    }
}
