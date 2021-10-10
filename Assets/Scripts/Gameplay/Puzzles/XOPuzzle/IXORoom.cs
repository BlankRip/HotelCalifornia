using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.XO {
    public interface IXOSolutionRoom
    {
        void Solved();
    }

    public interface IXOPuzzleRoom
    {
        void SetSolution(List<string> solution);
    }
}