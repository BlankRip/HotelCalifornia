using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public interface ILevelSeed
    {
        int GetRandomBetween(int min, int max);
        void ClearCurrent();
        void UpdateSeed();
        void TurnOnGeneration();
        void TurnOffGeneration();
    }
}
