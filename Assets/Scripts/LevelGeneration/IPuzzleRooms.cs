using System.Collections.Generic;

namespace Knotgames.LevelGen {
    public enum PuzzleType { A, B, C, D, E, F, Nada };
    public interface IPairPuzzleRoom
    {
        IPuzzleActivator GetAndActivePuzzle(List<PuzzleType> myType);
        IPuzzleActivator ActivatePuzzleOfType(PuzzleType puzzleType);
        List<PuzzleType> GetPuzzleTypes();
        List<RoomType> GetConnectableRoomTypes();
    }

    public interface ISingelPuzzleRoom
    {
        PuzzleType GetAndActivePuzzle(ref List<PuzzleType> exclusionTypes);
        void SelfKill();
    }
}
