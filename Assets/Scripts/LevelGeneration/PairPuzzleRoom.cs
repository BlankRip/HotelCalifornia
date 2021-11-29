using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen {
    public class PairPuzzleRoom : MonoBehaviour, IPairPuzzleRoom
    {
        [SerializeField] List<RoomType> connectable;
        [SerializeField] List<PuzzleActivator> roomPuzzles;
        [SerializeField] Renderer renderer;
        private List<PuzzleType> puzzleTypes;
        private List<PuzzleType> connectablePuzzleTypes;
        private List<IPuzzleActivator> myPuzzles;

        public IPuzzleActivator GetAndActivePuzzle(List<PuzzleType> myType) {
            List<PuzzleType> common = new List<PuzzleType>();
            for (int i = 0; i < myType.Count; i++) {
                if(GetPuzzleTypes().Contains(myType[i]))
                common.Add(myType[i]);
            }

            if(common.Count > 1) {
                int rand = KnotRandom.theRand.Next(0, common.Count);
                return ActivatePuzzleOfType(common[rand]);
            } else {
                return ActivatePuzzleOfType(common[0]);
            }
        }

        public IPuzzleActivator ActivatePuzzleOfType(PuzzleType puzzleType) {
            for (int i = 0; i < myPuzzles.Count; i++) {
                if(myPuzzles[i].GetPuzzleType() == puzzleType) {
                    myPuzzles[i].ActivatePuzzle(renderer);
                    Debug.Log($"<color=pink> {myPuzzles[i].GetPuzzleType()} </color>", this.gameObject);
                    return myPuzzles[i];
                }
            }
            return null;
        }

        private List<PuzzleType> GetPuzzleTypes() {
            FillPuzzleInterface();
            FillPuzzleTypes();

            return puzzleTypes;
        }

        public List<PuzzleType> GetConnectablePuzzleTypes() {
            FillPuzzleInterface();
            FillConnectablePuzzleTypes();

            return connectablePuzzleTypes;
        }

        private void FillPuzzleInterface() {
            if(myPuzzles == null) {
                myPuzzles = new List<IPuzzleActivator>();
                for (int i = 0; i < roomPuzzles.Count; i++)
                    myPuzzles.Add(roomPuzzles[i]);
            }
        }

        private void FillPuzzleTypes() {
            if(puzzleTypes == null || puzzleTypes.Count == 0) {
                puzzleTypes = new List<PuzzleType>();
                for (int i = 0; i < myPuzzles.Count; i++)
                    puzzleTypes.Add(myPuzzles[i].GetPuzzleType());
            }
        }

        private void FillConnectablePuzzleTypes() {
            if(connectablePuzzleTypes == null || connectablePuzzleTypes.Count == 0) {
                connectablePuzzleTypes = new List<PuzzleType>();
                for (int i = 0; i < myPuzzles.Count; i++)
                    connectablePuzzleTypes.Add(myPuzzles[i].GetConnectableType());
            }
        }

        public List<RoomType> GetConnectableRoomTypes() {
            return connectable;
        }
    }
}