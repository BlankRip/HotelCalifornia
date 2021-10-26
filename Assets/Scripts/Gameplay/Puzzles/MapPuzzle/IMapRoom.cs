using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Map {
    public interface IMapSolutionRoom
    {
        void Solved();
    }

    public interface IMapPuzzleRoom
    {
        void SetSolution(List<bool> solution);
    }
}