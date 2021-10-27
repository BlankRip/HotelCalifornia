using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Map
{
    [CreateAssetMenu()]
    public class MapManager : ScriptableObject
    {
        public MapPiece previousPiece;
        public Material[] materials;
    }
}