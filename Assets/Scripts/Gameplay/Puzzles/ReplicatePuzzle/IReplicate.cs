using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Replicate
{
    public interface IReplicateSolution
    {
        List<string> BuildNewSolution(Transform newSpot);
        List<RepObj> GetStoredObjs();
    }

    public interface IReplicatePuzzle
    {
        void SetUp(IReplicateSolutionRoom solutionRoom);
        void SetSolution(List<string> sol);
        void CheckSolution();
    }

    public interface IReplicateSlot
    {
        string GetValue();
        void SetNull();
        void SetCollider(bool val);
    }

    public interface IReplicateObject
    {
        void Pick();
        void Drop();
        void Drop(bool overrider);
        string GetName();
        void HandleSlotting(IReplicateSlot slot, Vector3 pos, Quaternion rot);
    }
}