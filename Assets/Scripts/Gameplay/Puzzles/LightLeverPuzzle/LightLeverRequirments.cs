using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.LevelLight {
    public class SolutionData {
        public LightColour colour;
        public int amount;
    }

    public enum LightColour {
        NRed, Green, Blue, Yellow, White, Pink
    }
}