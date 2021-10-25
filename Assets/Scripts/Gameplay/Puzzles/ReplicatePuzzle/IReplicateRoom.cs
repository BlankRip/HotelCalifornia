using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Replicate
{
    public interface IReplicateSolutionRoom
    {
        void Solved();
        void ResyncSolution(List<string> sol, List<RepObj> toSpawn);
        ReplicateObjectSlot GetCorrespondingSlot(int index);
    }

    public interface IReplicatePuzzleRoom
    {
        void SetSolution(List<string> solution);
        void SpawnObjs(List<RepObj> objs);
    }
}