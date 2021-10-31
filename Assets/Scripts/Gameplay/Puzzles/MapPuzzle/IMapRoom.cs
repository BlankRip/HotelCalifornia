using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Map {
    public interface IMapSolutionRoom
    {
        bool CheckSolution();
        void ResyncSolution(List<bool> connections);
    }

    public interface IMapPuzzleRoom
    {
        void SetSolution(List<bool> solution);
    }
}