using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Morse {
    [CreateAssetMenu()]
    public class ScriptableMorsePuzzle : ScriptableObject
    {
        public IMorsePuzzle manager;
    }
}