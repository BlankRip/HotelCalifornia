using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public class Room : MonoBehaviour, IRoom
    {
        [SerializeField] RoomType roomType;
        [SerializeField] List<Transform> doorways;
        [SerializeField] List<Collider> colliders;
        [SerializeField] GameObject myPuzzleVariant;
        [SerializeField] GameObject mySinglePuzzleVariant;
        private List<Bounds> roomBounds;
        private List<Transform> doorTransforms;

        public void SelfKill() {
            Destroy(this.gameObject);
        }

        public List<Transform> GetDoorways() {
            return doorways;
        }

        public List<Bounds> GetRoomBounds() {
            if(roomBounds == null) {
                roomBounds = new List<Bounds>();
                for(int i = 0; i < colliders.Count; i++)
                    roomBounds.Add(colliders[i].bounds);
            }

            return roomBounds;
        }

        public RoomType GetRoomType() {
            return roomType;
        }

        public GameObject GetPuzzleVarient() {
            return myPuzzleVariant;
        }

        public GameObject GetSingleVarient() {
            return mySinglePuzzleVariant;
        }
    }
}