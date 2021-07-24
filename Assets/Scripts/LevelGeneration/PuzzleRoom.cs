using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public class PuzzleRoom : MonoBehaviour, IPuzzleRoom
    {
        [SerializeField] ScriptableLevelSeed seeder;
        [SerializeField] List<RoomType> connectable;
        [SerializeField] List<Puzzle> roomPuzzles;
        [SerializeField] Renderer renderer;
        private List<PuzzleType> puzzleTypes;
        private List<IPuzzle> myPuzzles;

        public PuzzleType GetAndActivePuzzle(List<PuzzleType> myType, ref List<PuzzleType> exclusionTypes) {
            List<PuzzleType> common = new List<PuzzleType>();
            for (int i = 0; i < myType.Count; i++) {
                if(GetPuzzleTypes().Contains(myType[i]))
                common.Add(myType[i]);
            }

            if(common.Count > 1) {
                int rand = seeder.levelSeed.GetRandomBetween(0, common.Count);
                ActivatePuzzleOfType(common[rand]);
                return common[rand];
            } else {
                ActivatePuzzleOfType(common[0]);
                return common[0];
            }
        }

        private void ActivatePuzzleOfType(PuzzleType puzzleType) {
            for (int i = 0; i < myPuzzles.Count; i++) {
                if(myPuzzles[i].GetPuzzleType() == puzzleType) {
                    myPuzzles[i].ActivatePuzzle(renderer);
                    break;
                }
            }
        }

        public List<PuzzleType> GetPuzzleTypes() {
            FillPuzzleInterface();
            FillPuzzleTypes();

            return puzzleTypes;
        }

        private void FillPuzzleInterface() {
            if(myPuzzles == null) {
                myPuzzles = new List<IPuzzle>();
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
    }
}