using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Replicate
{
    public interface IReplicateSolutionRoom
    {
        void Solved();
        GameObject GetMyGO();
    }

    public interface IReplicatePuzzleRoom
    {
        void SetSolution(List<string> solution);
        void SpawnObjs(List<RepObj> objs);
    }
}