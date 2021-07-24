using System.Collections.Generic;

namespace Knotgames.Blank.LevelGen {
    public enum PuzzleType { A, B, C, D, E, F, Nada };
    public interface IPuzzleRoom
    {
        List<PuzzleType> GetPuzzleTypes();
        PuzzleType GetAndActivePuzzle(List<PuzzleType> myType, ref List<PuzzleType> exclusionTypes);
    }
}
