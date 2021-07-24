using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public class SinglePuzzleRoom : MonoBehaviour, IPuzzleRoom
    {
        [SerializeField] ScriptableLevelSeed seeder;
        public List<Puzzle> roomPuzzles;
        [SerializeField] private Renderer renderer;

        public PuzzleType GetAndActivePuzzle(List<PuzzleType> myType, ref List<PuzzleType> exclusionTypes) {
            int rand = seeder.levelSeed.GetRandomBetween(0, roomPuzzles.Count);
            if(!exclusionTypes.Contains(roomPuzzles[rand].GetPuzzleType())) {
                roomPuzzles[rand].ActivatePuzzle(renderer);
                exclusionTypes.Add(roomPuzzles[rand].GetPuzzleType());
                return roomPuzzles[rand].GetPuzzleType();
            } else {
                return PuzzleType.Nada;
            }
        }

        public List<PuzzleType> GetPuzzleTypes() {
            throw new System.NotImplementedException();
        }
    }
}
