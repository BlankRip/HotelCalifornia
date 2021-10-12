using System.Collections;
using System.Collections.Generic;
using Knotgames.Audio;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Morse {
    public interface IMorsePlayerRoom
    {
        void Solved();
    }

    public interface IMorsePuzzleRoom
    {
        void SetSolution(List<ClipName> solution);
    }
}