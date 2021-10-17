using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Replicate
{
    public interface IReplicateSolution
    {
        List<string> BuildNewSolution(Transform newSpot);
    }

    public interface IReplicatePuzzle
    {
        void SetUp(IReplicateSolutionRoom solutionRoom);
        void SetSolution(List<string> sol);
        void CheckSolution();
    }

    public interface IReplicateSlot
    {

    }

    public interface IReplicateObject
    {

    }
}