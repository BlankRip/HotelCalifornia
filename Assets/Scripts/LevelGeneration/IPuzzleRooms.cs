using System.Collections.Generic;

namespace Knotgames.LevelGen {
    public enum PuzzleType {
        ReplicateA, ReplicateB, ReplicateC,
        MazeTV, MazeEntry, 
        LightsLever, 
        MapLines, MapDots, 
        RadioFrequency, RadioInputs,
        XOInteractable, XOAnswer,
        
        ChemicalRoom, QuizeRoom, MoresRoom,
        Nada
    };
    public interface IPairPuzzleRoom
    {
        IPuzzleActivator GetAndActivePuzzle(List<PuzzleType> myType);
        IPuzzleActivator ActivatePuzzleOfType(PuzzleType puzzleType);
        List<PuzzleType> GetConnectablePuzzleTypes();
        List<RoomType> GetConnectableRoomTypes();
    }

    public interface ISingelPuzzleRoom
    {
        PuzzleType GetAndActivePuzzle(ref List<PuzzleType> exclusionTypes);
        void SelfKill();
    }
}
