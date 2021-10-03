using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    [CreateAssetMenu()]
    public class ScriptatbleChemicalPuzzle: ScriptableObject
    {
        public IChemicalPuzzle manager;
    }
}