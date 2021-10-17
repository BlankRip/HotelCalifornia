using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Riddler {
    [CreateAssetMenu()]
    public class ScriptableRiddlerPuzzle : ScriptableObject
    {
        public IRiddlerPuzzle manager;
    }
}