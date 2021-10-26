using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.LeverLight {
    public class SolutionData {
        public LightColour colour;
        public int amount;
    }

    public enum LightColour {
        Red, Green, Blue, Yellow, Cream, Purple
    }
}