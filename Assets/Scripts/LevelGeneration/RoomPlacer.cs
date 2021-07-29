using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public class RoomPlacer: IRoomPlacer
    {
        ScriptableLevelSeed seeder;
        BuilderData builderData;
        BuildingStatus currentBuildingData;

        public RoomPlacer(ScriptableLevelSeed levelSeed, BuilderData builderData, BuildingStatus buildingStatus) {
            seeder = levelSeed;
            this.builderData = builderData;
            currentBuildingData = buildingStatus;
        }

        public bool PlaceRoom(List<GameObject> availableRooms) {
            List<GameObject> available = new List<GameObject>(availableRooms);
            return PlaceRoom(ref available);
        }

        public bool PlaceRoom(ref List<GameObject> availableRooms) {
            int rand = seeder.levelSeed.GetRandomBetween(0, availableRooms.Count - 1, SeedValueType.PickRoom);

            GameObject spawned = GameObject.Instantiate(availableRooms[rand], Vector3.zero, Quaternion.identity);
            spawned.transform.parent = builderData.parent;
            IRoom current = spawned.GetComponent<IRoom>();

            List<Transform> currentDoors = new List<Transform>();
            AddDoorwaysToList(current, ref currentDoors);

            bool roomPlaced = TryPlaceRoomAtDoors(current, ref currentDoors);

            //! if no room could connect anywhere then will have to restart the level generator
            if(roomPlaced) {
                currentBuildingData.currentRoutRooms.Add(current);
                currentBuildingData.eachTypeSpawned[currentBuildingData.allRoomTypes.IndexOf(current.GetRoomType())]++;
                availableRooms.RemoveAt(rand);
                return true;
            } else {
                current.SelfKill();
                return false;
            }
        }

        private void AddDoorwaysToList(IRoom room, ref List<Transform> availableDoorways) {
            List<Transform> doorways = room.GetDoorways();
            for (int i = 0; i < doorways.Count; i++) {
                //! int rand = seeder.levelSeed.GetRandomBetween(0, availableDoorways.Count, SeedValueType.DoorInsersion);
                //! availableDoorways.Insert(rand, doorways[i]);
                availableDoorways.Add(doorways[i]);
            }
        }

        private bool TryPlaceRoomAtDoors(IRoom room, ref List<Transform> currentDoors) {
            bool roomPlaced = false;
            for (int i = 0; i < currentBuildingData.availableDoorways.Count; i++) {
                for (int j = 0; j < currentDoors.Count; j++) {
                    Transform roomTransform = room.GetTransform();
                    PlaceRoomAtDoor(ref roomTransform, currentDoors[j], currentBuildingData.availableDoorways[i]);

                    if(CheckRoomOverlap(room))
                        continue;
                    
                    roomPlaced = true;

                    currentDoors[j].gameObject.SetActive(false);
                    currentDoors.RemoveAt(j);
                    currentBuildingData.availableDoorways[i].gameObject.SetActive(false);
                    UpdateAvailableDoors(currentDoors);
                    
                    break;
                }
                if(roomPlaced)
                    break;
            }
            return roomPlaced;
        }

        private void PlaceRoomAtDoor(ref Transform room, Transform myDoor, Transform attachTo) {
            //^ Making door face opposit to each other
            room.rotation = Quaternion.identity;
            Vector3 targetDoorEular = attachTo.eulerAngles;
            Vector3 myDoorEular = myDoor.eulerAngles;
            float deltaAngel = Mathf.DeltaAngle(myDoorEular.y, targetDoorEular.y);
            Quaternion targetRotation = Quaternion.AngleAxis(deltaAngel, Vector3.up);
            room.rotation = targetRotation * Quaternion.Euler(0, 180, 0);

            //^ Positioning the Room first get the difference between room center and doorway
            Vector3 roomPosOffset = myDoor.position - room.position;
            room.position = attachTo.position - roomPosOffset;
        }

        private bool CheckRoomOverlap(IRoom room) {
            List<Bounds> bounds = room.GetRoomBounds();
            List<Collider> roomColliders = room.GetColliders();
            for (int i = 0; i < bounds.Count; i++) {
                Vector3 size = bounds[i].size;
                //bounds[i].Expand(-0.1f);
                size.x -= 0.1f;
                size.y -= 0.1f;
                size.z -= 0.1f;
                Collider[] colliders = Physics.OverlapBox(roomColliders[i].transform.position, size/2, room.GetTransform().rotation, builderData.roomLayerMask);
                //*testing.Add(new test(room.colliders[i].transform.position, size));
                
                //! can avoid the for loop below by only making the room colliders to be on the roomLayerMask
                if(colliders.Length > 0) {
                    for (int j = 0; j < colliders.Length; j++) {
                        if(colliders[j].transform.parent == room.GetTransform())
                            continue;
                        else {
                            //Debug.Log("Overlapped");
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void UpdateAvailableDoors(List<Transform> updateWith) {
            currentBuildingData.availableDoorways.Clear();
            currentBuildingData.availableDoorways = new List<Transform>(updateWith);
        }
    }
}