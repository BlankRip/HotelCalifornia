using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public class SinglePuzzlePlacer: IPuzzlePlacer
    {
        private ScriptableLevelSeed seeder;
        private BuildingStatus currentBuildStatus;
        private List<PuzzleType> spawnedSingleTypes;
        private int setInUse;

        public SinglePuzzlePlacer(ScriptableLevelSeed levelSeed, ref BuildingStatus currentBuildStatus) {
            seeder = levelSeed;
            this.currentBuildStatus = currentBuildStatus;

            spawnedSingleTypes = new List<PuzzleType>();
        }

        public bool Place() {
            bool puzzlePlaced = false;
            while(!puzzlePlaced) {
                List<IRoom> currentSet = GetRandomSet();
                int rand = seeder.levelSeed.GetRandomBetween(0, currentSet.Count);
                ISingelPuzzleRoom spawned = null;
                try {
                    spawned = GameObject.Instantiate(currentSet[rand].GetSingleVarient(), 
                    currentSet[rand].GetTransform().position, currentSet[rand].GetTransform().rotation,
                    currentSet[rand].GetTransform().parent).GetComponent<ISingelPuzzleRoom>();

                } catch {
                    Debug.Log("out");
                }
                
                if(spawned == null) {
                    Debug.Log("To Break");
                }

                PuzzleType validPuzzle = spawned.GetAndActivePuzzle(ref spawnedSingleTypes);
                if(validPuzzle == PuzzleType.Nada) {
                    spawned.SelfKill();
                } else {
                    CleanUpReplacedRoom(currentSet[rand], setInUse);
                    puzzlePlaced = true;
                }
            }
            seeder.levelSeed.UpdateSeed();
            return true;
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
