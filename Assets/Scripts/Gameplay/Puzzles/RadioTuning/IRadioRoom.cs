using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Radio {
    public interface IRadioSolutionRoom
    {
        void Solved();
    }

    public interface IRadioPuzzleRoom
    {
        void SetSolution(List<string> solution);
    }
}