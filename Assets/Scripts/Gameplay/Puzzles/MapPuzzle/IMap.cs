using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Map
{
    public interface IMapSolution
    {
        void SetupMap();
        void AddConnection(MapPiece A, MapPiece B);
        List<bool> BuildNewSolution(Transform newSpot);
        List<string> GetConnectionValues();
        void Solved();
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