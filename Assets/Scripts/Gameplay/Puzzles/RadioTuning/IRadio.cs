using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Radio
{
    public interface IRadioSolution
    {
        void SetupRadio();
        List<string> BuildNewSolution(Transform newSpot);
    }

    public interface IRadioTuner
    {
        void SetUp(IRadioSolutionRoom solutionRoom);
        void SetSolution(List<string> sol);
        void CheckSolution();
    }
}