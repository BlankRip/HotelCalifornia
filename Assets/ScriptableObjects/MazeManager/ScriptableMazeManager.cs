using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Maze {
    [CreateAssetMenu()]
    public class ScriptableMazeManager : ScriptableObject
    {
        public IMazeManager manager;
    }
}