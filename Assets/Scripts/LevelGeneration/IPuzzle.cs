using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public interface IPuzzle 
    {
        PuzzleType GetPuzzleType();
        void ActivatePuzzle(Renderer renderer);
    }
}
