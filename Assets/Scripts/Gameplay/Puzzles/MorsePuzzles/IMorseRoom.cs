using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Audio;

namespace Knotgames.Gameplay.Puzzle.Morse
{
    public interface IMorseRoom
    {
        List<char> GetSolution();
        Dictionary<char, AudioData> GetSolutionDictianary();
        void Solved();
    }
}