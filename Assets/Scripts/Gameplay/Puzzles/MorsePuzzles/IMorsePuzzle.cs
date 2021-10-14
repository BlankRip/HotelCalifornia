using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Audio;

namespace Knotgames.Gameplay.Puzzle.Morse
{
    public interface IMorsePuzzle
    {
        List<char> GetSolution();
        Dictionary<char, AudioData> GetSolutionDictionary();
    }
}