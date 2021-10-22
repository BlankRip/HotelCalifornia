using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Maze {
    [CreateAssetMenu()]
    public class ScriptableMazeRoom : ScriptableObject
    {
        public IMazeEntryRoom manager;
    }
}