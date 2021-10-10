using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.XO {
    public interface ISolutionBoard
    {
        void SetUpBoard();
        List<string> BuildNewSolution(Transform newSpot);
    }
}