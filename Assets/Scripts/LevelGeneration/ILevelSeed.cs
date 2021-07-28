using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {

    public enum SeedValueType { 
        RoutPick, CorridorCount, PickRoom,
        PickPuzzleRoom, PickRoutId, PickPuzzle
    }
    public interface ILevelSeed
    {
        int GetRandomBetween(int min, int max, SeedValueType valueType);
        void ClearCurrent();
        void UpdateSeed();
        void TurnOnGeneration();
        void TurnOffGeneration();
    }
}
