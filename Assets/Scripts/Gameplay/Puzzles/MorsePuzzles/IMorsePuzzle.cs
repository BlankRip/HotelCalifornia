using System.Collections;
using System.Collections.Generic;
using Knotgames.Audio;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Morse {
    public interface IMorseSolutionPlayer
    {
        void SetUpPlayer();
        void Solved();
        void Link(MorsePuzzle puzzle);
    }

    public interface IMorsePuzzle
    {
        bool CheckSolution();
        void Solved();
    }
}