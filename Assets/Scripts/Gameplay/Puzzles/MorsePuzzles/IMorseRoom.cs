using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Morse
{
    public interface IMorseRoom
    {
        List<char> GetSolution();
    }
}