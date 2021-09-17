using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen {
    public class PuzzleRoom : MonoBehaviour, IPairPuzzleRoom
    {
        [SerializeField] List<RoomType> connectable;
        [SerializeField] List<Puzzle> roomPuzzles;
        [SerializeField] Renderer renderer;
        private List<PuzzleType> puzzleTypes;
        private List<IPuzzleActivator> myPuzzles;

        public IPuzzleActivator GetAndActivePuzzle(List<PuzzleType> myType) {
            List<PuzzleType> common = new List<PuzzleType>();
            for (int i = 0; i < myType.Count; i++) {
                if(GetPuzzleTypes().Contains(myType[i]))
                common.Add(myType[i]);
            }

            if(common.Count > 1) {
                int rand = Random.Range(0, common.Count);
                return ActivatePuzzleOfType(common[rand]);
            } else {
                return ActivatePuzzleOfType(common[0]);
            }
        }

        public IPuzzleActivator ActivatePuzzleOfType(PuzzleType puzzleType) {
            for (int i = 0; i < myPuzzles.Count; i++) {
                if(myPuzzles[i].GetPuzzleType() == puzzleType) {
                    myPuzzles[i].ActivatePuzzle(renderer);
                    return myPuzzles[i];
                }
            }
            return null;
        }

        public List<PuzzleType> GetPuzzleTypes() {
            FillPuzzleInterface();
            FillPuzzleTypes();

            return puzzleTypes;
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

        public List<RoomType> GetConnectableRoomTypes() {
            return connectable;
        }
    }
}