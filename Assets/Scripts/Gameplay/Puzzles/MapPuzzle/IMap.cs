using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Map
{
    public interface IMapSolution
    {
        void SetupMap();
        List<bool> BuildNewSolution(Transform newSpot);
        List<string> GetConnectionValues();
    }

    public interface IMapPuzzle
    {
        void SetUp(IMapSolutionRoom solutionRoom);
        void SetSolution(List<bool> sol);
        void CheckSolution();
    }

    public interface IMapPiece
    {
        bool GetValue();
    }
}