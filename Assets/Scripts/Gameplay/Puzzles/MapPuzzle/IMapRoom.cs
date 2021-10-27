using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Map {
    public interface IMapSolutionRoom
    {
        bool Solved();
        void ResyncSolution(List<string> connections);
        bool CheckMySol();
    }

    public interface IMapPuzzleRoom
    {
        void SetSolution(List<bool> solution);
    }
}