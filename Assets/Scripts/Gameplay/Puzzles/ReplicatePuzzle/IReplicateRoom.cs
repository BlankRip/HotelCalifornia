using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Replicate
{
    public interface IReplicateSolutionRoom
    {
        void Solved();
    }

    public interface IReplicatePuzzleRoom
    {
        void SetSolution(List<string> solution);
    }
}